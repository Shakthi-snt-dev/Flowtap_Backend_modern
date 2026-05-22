using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.Inventory.Queries.GetProductsByDeviceModel;

public record ProductBriefDto(
    Guid Id,
    Guid CompanyId,
    Guid CategoryId,
    string Name,
    string SKU,
    decimal DefaultSalePrice,
    bool IsActive);

public record GetProductsByDeviceModelQuery(
    Guid CompanyId,
    Guid DeviceModelId) : IRequest<Result<List<ProductBriefDto>>>;
