using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Commands.AssignTicket;

public record AssignTicketCommand(
    Guid CompanyId, Guid ServiceTicketId,
    Guid? ExecutorEmployeeId, Guid? ManagerEmployeeId) : IRequest<Result<bool>>;
