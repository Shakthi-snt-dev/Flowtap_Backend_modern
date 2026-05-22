using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Features.ServiceTickets.DTOs;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Queries.GetTicket;

public record GetTicketQuery(Guid CompanyId, Guid TicketId) : IRequest<Result<TicketDto>>;
