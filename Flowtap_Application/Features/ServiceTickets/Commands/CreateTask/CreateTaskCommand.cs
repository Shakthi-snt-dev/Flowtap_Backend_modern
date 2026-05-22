using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Commands.CreateTask;

public record CreateTaskCommand(
    Guid CompanyId, Guid LocationId, Guid AuthorEmployeeId,
    Guid AssigneeEmployeeId, string Title, string Description,
    string Priority, DateTime? Deadline, Guid? TicketId, List<string> Tags) : IRequest<Result<Guid>>;
