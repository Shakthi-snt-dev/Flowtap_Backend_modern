using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Features.ServiceTickets.Commands.CreateServiceCategory;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Queries.GetServiceCategories;

public record GetServiceCategoriesQuery(
    Guid CompanyId,
    Guid? ParentCategoryId,
    bool? IsActive) : IRequest<Result<List<ServiceCategoryDto>>>;
