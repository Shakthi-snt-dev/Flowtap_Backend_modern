using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Interfaces;
using Flowtap_Domain.BoundedContexts.Modules.Sales.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Flowtap_Application.Features.Sales.Queries.GetPayments;

public class GetPaymentsQueryHandler(IApplicationDbContext db)
    : IRequestHandler<GetPaymentsQuery, Result<PaginatedList<PaymentListItemDto>>>
{
    public async Task<Result<PaginatedList<PaymentListItemDto>>> Handle(
        GetPaymentsQuery request, CancellationToken ct)
    {
        var query = db.Payments
            .Include(p => p.Account)
            .Where(p => p.CompanyId == request.CompanyId);

        // Filter by store — join through Sale.LocationId or ServiceTicket.LocationId
        if (request.LocationId.HasValue)
        {
            var locId = request.LocationId.Value;
            var saleIdsAtLocation = db.Sales
                .Where(s => s.CompanyId == request.CompanyId && s.LocationId == locId)
                .Select(s => s.Id);
            var ticketIdsAtLocation = db.ServiceTickets
                .Where(t => t.CompanyId == request.CompanyId && t.LocationId == locId)
                .Select(t => t.Id);
            query = query.Where(p =>
                (p.SaleId != null   && saleIdsAtLocation.Contains(p.SaleId.Value)) ||
                (p.SaleId == null   && p.TicketId != null && ticketIdsAtLocation.Contains(p.TicketId.Value)));
        }

        if (request.TicketId.HasValue)
            query = query.Where(p => p.TicketId == request.TicketId.Value);

        if (request.SaleId.HasValue)
            query = query.Where(p => p.SaleId == request.SaleId.Value);

        if (!string.IsNullOrWhiteSpace(request.Method) &&
            Enum.TryParse<PaymentMethod>(request.Method, true, out var method))
            query = query.Where(p => p.Method == method);

        if (!string.IsNullOrWhiteSpace(request.Purpose) &&
            Enum.TryParse<PaymentPurpose>(request.Purpose, true, out var purpose))
            query = query.Where(p => p.Purpose == purpose);

        if (request.DateFrom.HasValue)
            query = query.Where(p => p.PaidAt >= request.DateFrom.Value);

        if (request.DateTo.HasValue)
            query = query.Where(p => p.PaidAt <= request.DateTo.Value);

        var total = await query.CountAsync(ct);

        var payments = await query
            .OrderByDescending(p => p.PaidAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(ct);

        // ── Batch-load ticket numbers ─────────────────────────────────────────────
        var ticketIds = payments
            .Where(p => p.TicketId.HasValue)
            .Select(p => p.TicketId!.Value)
            .Distinct()
            .ToList();

        var ticketNumbers = ticketIds.Count > 0
            ? await db.ServiceTickets
                .Where(t => ticketIds.Contains(t.Id))
                .ToDictionaryAsync(t => t.Id, t => t.TicketNumber ?? string.Empty, ct)
            : new Dictionary<Guid, string>();

        // ── Batch-load sale transaction numbers ───────────────────────────────────
        var saleIds = payments
            .Where(p => p.SaleId.HasValue)
            .Select(p => p.SaleId!.Value)
            .Distinct()
            .ToList();

        var saleTxNumbers = saleIds.Count > 0
            ? await db.Sales
                .Where(s => saleIds.Contains(s.Id))
                .ToDictionaryAsync(s => s.Id, s => s.TransactionNumber ?? string.Empty, ct)
            : new Dictionary<Guid, string>();

        var items = payments.Select(p => new PaymentListItemDto(
            p.Id,
            p.Amount,
            p.Method.ToString(),
            p.Purpose.ToString(),
            p.Account?.Name ?? p.Method.ToString(),
            p.Account?.Type.ToString() ?? string.Empty,
            p.TicketId,
            p.TicketId.HasValue && ticketNumbers.TryGetValue(p.TicketId.Value, out var tn) ? tn : null,
            p.SaleId,
            p.SaleId.HasValue && saleTxNumbers.TryGetValue(p.SaleId.Value, out var sn) ? sn : null,
            p.ExternalReference,
            p.Comment,
            p.PaidAt
        )).ToList();

        return Result<PaginatedList<PaymentListItemDto>>.Success(
            new PaginatedList<PaymentListItemDto>(items, total, request.Page, request.PageSize));
    }
}
