using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.Inventory.Commands.UpdateDeviceModel;

public record UpdateDeviceModelCommand(
    Guid Id, Guid CompanyId,
    string? Name, string? ImageUrl, bool? IsActive) : IRequest<Result<bool>>;
