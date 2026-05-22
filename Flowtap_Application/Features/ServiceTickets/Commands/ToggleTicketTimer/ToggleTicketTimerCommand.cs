using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Commands.ToggleTicketTimer;

public record ToggleTicketTimerCommand(Guid CompanyId, Guid EmployeeId, Guid TicketId) : IRequest<Result<bool>>;
