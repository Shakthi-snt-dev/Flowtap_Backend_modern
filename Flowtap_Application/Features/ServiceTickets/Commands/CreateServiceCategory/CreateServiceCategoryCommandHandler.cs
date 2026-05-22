using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Interfaces;
using Flowtap_Domain.BoundedContexts.Modules.ServiceTickets.Entities;
using MediatR;

namespace Flowtap_Application.Features.ServiceTickets.Commands.CreateServiceCategory;

public class CreateServiceCategoryCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateServiceCategoryCommand, Result<ServiceCategoryDto>>
{
    public async Task<Result<ServiceCategoryDto>> Handle(CreateServiceCategoryCommand request, CancellationToken ct)
    {
        var category = new ServiceCategory
        {
            CompanyId = request.CompanyId,
            ParentCategoryId = request.ParentCategoryId,
            Name = request.Name,
            Description = request.Description,
            IconUrl = request.IconUrl,
            SortOrder = request.SortOrder,
            IsActive = true
        };

        context.ServiceCategories.Add(category);
        await context.SaveChangesAsync(ct);

        return Result<ServiceCategoryDto>.Success(new ServiceCategoryDto(
            category.Id,
            category.CompanyId,
            category.ParentCategoryId,
            category.Name,
            category.Description,
            category.IconUrl,
            category.SortOrder,
            category.IsActive));
    }
}
