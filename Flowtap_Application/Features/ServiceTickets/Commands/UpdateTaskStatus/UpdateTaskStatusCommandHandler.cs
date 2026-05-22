using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Exceptions;
using Flowtap_Application.Common.Interfaces;
using Flowtap_Domain.BoundedContexts.Modules.ServiceTickets.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Flowtap_Application.Features.ServiceTickets.Commands.UpdateTaskStatus;

public class UpdateTaskStatusCommandHandler(IApplicationDbContext db)
    : IRequestHandler<UpdateTaskStatusCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateTaskStatusCommand request, CancellationToken ct)
    {
        var task = await db.WorkTasks
            .FirstOrDefaultAsync(t => t.Id == request.TaskId && t.CompanyId == request.CompanyId, ct)
            ?? throw new NotFoundException("WorkTask", request.TaskId);

        if (!Enum.TryParse<Flowtap_Domain.BoundedContexts.Modules.ServiceTickets.Enums.TaskStatus>(request.Status, true, out var newStatus))
            return Result<bool>.Failure($"Invalid status: {request.Status}");

        task.Status = newStatus;

        if (newStatus == Flowtap_Domain.BoundedContexts.Modules.ServiceTickets.Enums.TaskStatus.Done)
        {
            task.CompletedAt = DateTime.UtcNow;
        }
        else
        {
            task.CompletedAt = null;
        }

        await db.SaveChangesAsync(ct);
        return Result<bool>.Success(true);
    }
}
