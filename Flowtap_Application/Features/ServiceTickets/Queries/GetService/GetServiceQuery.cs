using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Features.ServiceTickets.DTOs;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Queries.GetService;

public record GetServiceQuery(Guid CompanyId, Guid ServiceId) : IRequest<Result<ServiceDto>>;
