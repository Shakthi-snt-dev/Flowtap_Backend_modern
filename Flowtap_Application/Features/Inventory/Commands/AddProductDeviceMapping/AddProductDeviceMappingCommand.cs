using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.Inventory.Commands.AddProductDeviceMapping;

public record AddProductDeviceMappingCommand(Guid CompanyId, Guid ProductId, Guid DeviceModelId)
    : IRequest<Result<Guid>>;
