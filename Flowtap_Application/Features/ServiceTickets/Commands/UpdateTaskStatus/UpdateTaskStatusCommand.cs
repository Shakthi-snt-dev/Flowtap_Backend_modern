using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Commands.UpdateTaskStatus;

public record UpdateTaskStatusCommand(
    Guid CompanyId,
    Guid TaskId,
    string Status) : IRequest<Result<bool>>;
