using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Features.ServiceTickets.DTOs;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Queries.GetTasks;

public record GetTasksQuery(
    Guid CompanyId,
    Guid? TicketId = null,
    Guid? AssigneeEmployeeId = null,
    Guid? AuthorEmployeeId = null,
    string? Status = null) : IRequest<Result<List<TaskDto>>>;
