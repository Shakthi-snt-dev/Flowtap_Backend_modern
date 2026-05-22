using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Exceptions;
using Flowtap_Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Flowtap_Application.Features.ServiceTickets.Commands.RemoveTicketItem;

public class RemoveTicketItemCommandHandler(IApplicationDbContext db)
    : IRequestHandler<RemoveTicketItemCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(RemoveTicketItemCommand request, CancellationToken ct)
    {
        var item = await db.ServiceTicketItems
            .FirstOrDefaultAsync(i => i.Id == request.ItemId && i.TicketId == request.TicketId, ct)
            ?? throw new NotFoundException("ServiceTicketItem", request.ItemId);

        var ticket = await db.ServiceTickets
            .FirstOrDefaultAsync(t => t.Id == request.TicketId && t.CompanyId == request.CompanyId, ct)
            ?? throw new NotFoundException("ServiceTicket", request.TicketId);

        // Subtract this item's contribution from TotalCost
        var lineNet   = item.Price * item.Quantity - item.DiscountAmount;
        var lineTotal = lineNet * (1 + item.TaxPercent / 100);

        if (ticket.Financials is not null)
            ticket.Financials.TotalCost = Math.Max(0, Math.Round(ticket.Financials.TotalCost - lineTotal, 2));

        db.ServiceTicketItems.Remove(item);
        await db.SaveChangesAsync(ct);
        return Result<bool>.Success(true);
    }
}
