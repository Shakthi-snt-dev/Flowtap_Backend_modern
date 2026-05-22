using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Interfaces;
using Flowtap_Domain.BoundedContexts.Modules.ServiceTickets.Entities;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Commands.CreateService;

public class CreateServiceCommandHandler(IApplicationDbContext db)
    : IRequestHandler<CreateServiceCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateServiceCommand request, CancellationToken ct)
    {
        var service = new Service
        {
            CompanyId = request.CompanyId,
            Name = request.Name,
            BasePrice = request.BasePrice,
            ServiceCategoryId = request.ServiceCategoryId,
            Description = request.Description,
            EstimatedDuration = request.EstimatedDuration,
            TaxSlabId = request.TaxSlabId,
            RequiresInventory = request.RequiresInventory,
            InventoryProductId = request.InventoryProductId,
            IsUniversal = request.IsUniversal,
            IsActive = true
        };

        db.Services.Add(service);
        await db.SaveChangesAsync(ct);
        return Result<Guid>.Success(service.Id);
    }
}
