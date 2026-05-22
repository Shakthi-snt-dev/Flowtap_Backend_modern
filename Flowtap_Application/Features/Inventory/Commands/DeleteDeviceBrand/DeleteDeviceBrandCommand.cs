using Flowtap_Application.Common.DTOs;
using MediatR;

namespace Flowtap_Application.Features.Inventory.Commands.DeleteDeviceBrand;

public record DeleteDeviceBrandCommand(Guid Id, Guid CompanyId) : IRequest<Result<bool>>;
