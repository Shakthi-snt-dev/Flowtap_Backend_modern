using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.Inventory.Commands.RemoveProductDeviceMapping;

public record RemoveProductDeviceMappingCommand(Guid CompanyId, Guid ProductId, Guid DeviceModelId)
    : IRequest<Result<bool>>;
