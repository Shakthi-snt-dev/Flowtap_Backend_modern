using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Interfaces;
using Flowtap_Application.Features.ServiceTickets.Commands.AddPartToTicket;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Flowtap_Application.Features.ServiceTickets.Queries.GetTicketParts;

public class GetTicketPartsQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetTicketPartsQuery, Result<List<TicketPartUsageDto>>>
{
    public async Task<Result<List<TicketPartUsageDto>>> Handle(GetTicketPartsQuery request, CancellationToken ct)
    {
        var parts = await context.ServiceTicketPartUsages
            .AsNoTracking()
            .Where(p => p.CompanyId == request.CompanyId && p.ServiceTicketId == request.ServiceTicketId)
            .ToListAsync(ct);

        var dtos = parts.Select(p => new TicketPartUsageDto(
            p.Id,
            p.ServiceTicketId,
            p.ProductId,
            p.WarehouseId,
            p.Quantity,
            p.UnitPrice,
            p.UsedAt)).ToList();

        return Result<List<TicketPartUsageDto>>.Success(dtos);
    }
}
