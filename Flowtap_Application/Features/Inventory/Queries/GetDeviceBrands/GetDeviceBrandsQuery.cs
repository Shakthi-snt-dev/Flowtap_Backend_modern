using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Features.Inventory.Commands.CreateDeviceBrand;
using MediatR;

namespace Flowtap_Application.Features.Inventory.Queries.GetDeviceBrands;

public record GetDeviceBrandsQuery(
    Guid? ProductCategoryId,
    string? SearchTerm,
    bool? IsActive) : IRequest<Result<List<DeviceBrandDto>>>;
