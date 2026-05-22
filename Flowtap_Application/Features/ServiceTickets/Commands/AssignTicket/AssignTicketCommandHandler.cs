using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Exceptions;
using Flowtap_Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Flowtap_Application.Features.ServiceTickets.Commands.AssignTicket;

public class AssignTicketCommandHandler(IApplicationDbContext db)
    : IRequestHandler<AssignTicketCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AssignTicketCommand request, CancellationToken ct)
    {
        var ticket = await db.ServiceTickets
            .FirstOrDefaultAsync(t => t.CompanyId == request.CompanyId && t.Id == request.ServiceTicketId, ct)
            ?? throw new NotFoundException(nameof(Flowtap_Domain.BoundedContexts.Modules.ServiceTickets.Entities.ServiceTicket), request.ServiceTicketId);

        if (request.ExecutorEmployeeId.HasValue)
            ticket.ExecutorEmployeeId = request.ExecutorEmployeeId;

        if (request.ManagerEmployeeId.HasValue)
            ticket.ManagerEmployeeId = request.ManagerEmployeeId;

        await db.SaveChangesAsync(ct);
        return Result<bool>.Success(true);
    }
}
