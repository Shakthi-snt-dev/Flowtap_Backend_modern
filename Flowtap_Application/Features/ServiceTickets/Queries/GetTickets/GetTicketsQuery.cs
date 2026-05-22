using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Features.ServiceTickets.DTOs;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Queries.GetTickets;

public record GetTicketsQuery(Guid CompanyId, Guid? LocationId, string? Status, int Page = 1, int PageSize = 20)
    : IRequest<Result<PaginatedList<TicketListDto>>>;
