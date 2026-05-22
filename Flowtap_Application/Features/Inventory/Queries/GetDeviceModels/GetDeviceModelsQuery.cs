using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Features.Inventory.Commands.CreateDeviceModel;
using MediatR;

namespace Flowtap_Application.Features.Inventory.Queries.GetDeviceModels;

public record GetDeviceModelsQuery(
    Guid? BrandId,
    Guid? ProductCategoryId,
    Guid? ParentModelId,
    string? SearchTerm,
    bool? IsActive) : IRequest<Result<List<DeviceModelDto>>>;
