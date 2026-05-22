using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.Inventory.Commands.DeleteDeviceModel;

public record DeleteDeviceModelCommand(Guid Id, Guid CompanyId) : IRequest<Result<bool>>;
