using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.Sales.Commands.CreateSale;

public record CreateSaleCommand(
    Guid CompanyId,
    Guid LocationId,
    Guid? ClientId,             // nullable — null = walk-in customer
    string Source,
    Guid? TicketId,
    string? Notes,
    string? IdempotencyKey,
    List<CreateSaleItemDto> Items,
    List<CreateSalePaymentDto>? Payments = null,  // inline payments at checkout
    Guid? EmployeeId = null                       // active cashier who processed the sale
) : IRequest<Result<Guid>>;

public record CreateSaleItemDto(
    Guid ProductId,
    string ProductName,
    string Type,
    decimal Quantity,
    decimal UnitPrice,
    decimal TaxPercent,
    decimal DiscountPercent,
    decimal DiscountAmount,
    string? SerialNumber = null);   // populated when item was added via serial scan

public record CreateSalePaymentDto(
    string Method,              // Cash | Card | UPI | NetBanking | Wallet
    decimal Amount,
    string Purpose = "Final",   // Advance | Partial | Final
    string? Reference = null);  // external ref / transaction ID
