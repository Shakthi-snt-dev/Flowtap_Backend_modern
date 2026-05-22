using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Commands.ToggleTaskTimer;

public record ToggleTaskTimerCommand(Guid CompanyId, Guid EmployeeId, Guid TaskId) : IRequest<Result<bool>>;
