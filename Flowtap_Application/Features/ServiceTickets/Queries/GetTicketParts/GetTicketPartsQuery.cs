using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Features.ServiceTickets.Commands.AddPartToTicket;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Queries.GetTicketParts;

public record GetTicketPartsQuery(
    Guid CompanyId,
    Guid ServiceTicketId) : IRequest<Result<List<TicketPartUsageDto>>>;
