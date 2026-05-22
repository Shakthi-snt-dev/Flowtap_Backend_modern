using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Features.ServiceTickets.DTOs;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Queries.GetServices;

public record GetServicesQuery(Guid CompanyId, int Page = 1, int PageSize = 20, string? Search = null)
    : IRequest<Result<PaginatedList<ServiceDto>>>;
