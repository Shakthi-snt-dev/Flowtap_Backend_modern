using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Interfaces;
using Flowtap_Domain.BoundedContexts.Modules.Inventory.Entities;
using MediatR;

namespace Flowtap_Application.Features.Inventory.Commands.CreateDeviceModel;

public class CreateDeviceModelCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateDeviceModelCommand, Result<DeviceModelDto>>
{
    public async Task<Result<DeviceModelDto>> Handle(CreateDeviceModelCommand request, CancellationToken ct)
    {
        var brand = await context.DeviceBrands.FindAsync(new object[] { request.BrandId }, ct);
        if (brand is null)
            return Result<DeviceModelDto>.Failure($"DeviceBrand with id '{request.BrandId}' was not found.");

        var model = new DeviceModel
        {
            BrandId = request.BrandId,
            ProductCategoryId = request.ProductCategoryId,
            ParentModelId = request.ParentModelId,
            Name = request.Name,
            ImageUrl = request.ImageUrl,
            IsActive = true
        };

        context.DeviceModels.Add(model);
        await context.SaveChangesAsync(ct);

        return Result<DeviceModelDto>.Success(new DeviceModelDto(
            model.Id,
            model.BrandId,
            brand.Name,
            model.ParentModelId,
            model.ProductCategoryId,
            model.Name,
            model.ImageUrl,
            model.IsActive));
    }
}
