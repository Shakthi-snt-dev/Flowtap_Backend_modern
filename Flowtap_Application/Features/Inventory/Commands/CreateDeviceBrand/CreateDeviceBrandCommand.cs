using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.Inventory.Commands.CreateDeviceBrand;

public record DeviceBrandDto(
    Guid Id,
    Guid? ProductCategoryId,
    string Name,
    string? IconUrl,
    string? Color,
    bool IsActive);

public record CreateDeviceBrandCommand(
    Guid? ProductCategoryId,
    string Name,
    string? IconUrl,
    string? Color) : IRequest<Result<DeviceBrandDto>>;
