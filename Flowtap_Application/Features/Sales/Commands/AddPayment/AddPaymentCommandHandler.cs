using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Exceptions;
using Flowtap_Application.Common.Interfaces;
using Flowtap_Domain.BoundedContexts.Modules.Sales.Entities;
using Flowtap_Domain.BoundedContexts.Modules.Sales.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Flowtap_Application.Features.Sales.Commands.AddPayment;

public class AddPaymentCommandHandler(IApplicationDbContext db, IDateTimeService dateTime)
    : IRequestHandler<AddPaymentCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddPaymentCommand request, CancellationToken ct)
    {
        // ── Idempotency ───────────────────────────────────────────────────────
        if (!string.IsNullOrWhiteSpace(request.IdempotencyKey))
        {
            var dup = await db.Payments
                .FirstOrDefaultAsync(p => p.IdempotencyKey == request.IdempotencyKey
                    && p.CompanyId == request.CompanyId, ct);
            if (dup is not null) return Result<Guid>.Success(dup.Id);
        }

        if (!Enum.TryParse<PaymentMethod>(request.Method, true, out var method))
            return Result<Guid>.Failure($"Invalid payment method: {request.Method}");

        if (!Enum.TryParse<PaymentPurpose>(request.Purpose, true, out var purpose))
            purpose = PaymentPurpose.Final;

        // ── Step 1: lightweight projection — get LocationId without tracking the sale.
        // ResolvePaymentAccountAsync may call SaveChangesAsync internally. If the full
        // Sale entity were tracked at that point, EF relationship-fixup would drag it
        // into the intermediate saves and corrupt its RowVersion (concurrency token).
        var saleInfo = await db.Sales
            .Where(s => s.Id == request.SaleId && s.CompanyId == request.CompanyId)
            .Select(s => new { s.LocationId, s.TotalAmount })
            .FirstOrDefaultAsync(ct)
            ?? throw new NotFoundException(nameof(Sale), request.SaleId);

        // ── Step 2: resolve account (intermediate SaveChangesAsync safe here) ──
        var accountId = request.AccountId;
        if (accountId == Guid.Empty)
        {
            accountId = await ResolvePaymentAccountAsync(
                request.CompanyId, saleInfo.LocationId, method, ct);
        }

        // ── Step 3: load sale with tracking + Payments already included.
        // Including Payments avoids a second SumAsync query later — that query would
        // auto-flush pending writes mid-handler, turning subsequent INSERTs into UPDATEs.
        var sale = await db.Sales
            .Include(s => s.History)
            .Include(s => s.Payments)
            .FirstOrDefaultAsync(s => s.Id == request.SaleId && s.CompanyId == request.CompanyId, ct)
            ?? throw new NotFoundException(nameof(Sale), request.SaleId);

        // ── Step 4: compute total paid in memory (no DB round-trip needed) ────
        var totalPaid = sale.Payments.Sum(p => p.Amount) + request.Amount;

        // ── Step 5: add payment + history, update status ──────────────────────
        var payment = new Payment
        {
            CompanyId         = request.CompanyId,
            SaleId            = sale.Id,
            Amount            = request.Amount,
            Method            = method,
            Purpose           = purpose,
            AccountId         = accountId,
            ExternalReference = request.ExternalReference,
            Comment           = request.Comment,
            EmployeeId        = request.EmployeeId,
            PaidAt            = dateTime.UtcNow,
            IdempotencyKey    = request.IdempotencyKey,
        };
        db.Payments.Add(payment);

        sale.History.Add(new SaleHistory
        {
            SaleId    = sale.Id,
            Message   = $"Payment of {request.Amount:C} received via {method} ({purpose}).",
            CreatedAt = dateTime.UtcNow,
        });

        if (totalPaid >= sale.TotalAmount)
        {
            sale.Status = SaleStatus.Completed;
            sale.History.Add(new SaleHistory
            {
                SaleId    = sale.Id,
                Message   = "Sale completed — fully paid.",
                CreatedAt = dateTime.UtcNow,
            });
        }

        // ── Step 6: single SaveChangesAsync ───────────────────────────────────
        await db.SaveChangesAsync(ct);
        return Result<Guid>.Success(payment.Id);
    }

    private async Task<Guid> ResolvePaymentAccountAsync(
        Guid companyId, Guid locationId, PaymentMethod method, CancellationToken ct)
    {
        var mapping = await db.PaymentMethodMappings
            .FirstOrDefaultAsync(m =>
                m.CompanyId == companyId &&
                m.LocationId == locationId &&
                m.Method == method, ct);

        if (mapping is not null) return mapping.PaymentAccountId;

        var accountType = method switch
        {
            PaymentMethod.Cash       => PaymentAccountType.Cash,
            PaymentMethod.Card       => PaymentAccountType.Bank,
            PaymentMethod.NetBanking => PaymentAccountType.Bank,
            _                        => PaymentAccountType.Gateway,
        };

        var account = await db.PaymentAccounts
            .FirstOrDefaultAsync(a =>
                a.CompanyId == companyId &&
                a.Type == accountType &&
                a.IsActive, ct);

        if (account is null)
        {
            account = new PaymentAccount
            {
                CompanyId = companyId,
                Name      = method.ToString(),
                Type      = accountType,
                IsActive  = true,
            };
            db.PaymentAccounts.Add(account);
            await db.SaveChangesAsync(ct);
        }

        db.PaymentMethodMappings.Add(new PaymentMethodMapping
        {
            CompanyId        = companyId,
            LocationId       = locationId,
            Method           = method,
            PaymentAccountId = account.Id,
        });
        await db.SaveChangesAsync(ct);

        return account.Id;
    }
}
