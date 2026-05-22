using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.Inventory.Commands.UpdateDeviceBrand;

public record UpdateDeviceBrandCommand(
    Guid Id, Guid CompanyId,
    string? Name, string? IconUrl, string? Color, bool? IsActive) : IRequest<Result<bool>>;
