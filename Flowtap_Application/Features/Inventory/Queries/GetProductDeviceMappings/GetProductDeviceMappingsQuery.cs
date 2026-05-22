using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.Inventory.Queries.GetProductDeviceMappings;

public record ProductDeviceMappingDto(Guid DeviceModelId, string ModelName, string? BrandName);

public record GetProductDeviceMappingsQuery(Guid CompanyId, Guid ProductId)
    : IRequest<Result<List<ProductDeviceMappingDto>>>;
