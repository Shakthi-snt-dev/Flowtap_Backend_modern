using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Exceptions;
using Flowtap_Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Flowtap_Application.Features.Inventory.Commands.DeleteDeviceModel;

public class DeleteDeviceModelCommandHandler(IApplicationDbContext db)
    : IRequestHandler<DeleteDeviceModelCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteDeviceModelCommand request, CancellationToken ct)
    {
        var model = await db.DeviceModels
            .FirstOrDefaultAsync(m => m.Id == request.Id, ct)
            ?? throw new NotFoundException(nameof(Flowtap_Domain.BoundedContexts.Modules.Inventory.Entities.DeviceModel), request.Id);

        model.IsActive = false;
        await db.SaveChangesAsync(ct);
        return Result<bool>.Success(true);
    }
}
