using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Features.ServiceTickets.DTOs;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Queries.GetServicesByModel;

public record GetServicesByModelQuery(Guid DeviceModelId, Guid CompanyId)
    : IRequest<Result<List<ServiceDto>>>;
