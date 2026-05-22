using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Flowtap_Application.Features.Inventory.Queries.GetProductsByDeviceModel;

public class GetProductsByDeviceModelQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetProductsByDeviceModelQuery, Result<List<ProductBriefDto>>>
{
    public async Task<Result<List<ProductBriefDto>>> Handle(GetProductsByDeviceModelQuery query, CancellationToken ct)
    {
        var productIds = await context.ProductDeviceModelMappings
            .Where(m => m.DeviceModelId == query.DeviceModelId)
            .Select(m => m.ProductId)
            .ToListAsync(ct);

        var products = await context.Products
            .Where(p => p.CompanyId == query.CompanyId && productIds.Contains(p.Id) && p.IsActive)
            .Select(p => new ProductBriefDto(
                p.Id,
                p.CompanyId,
                p.CategoryId,
                p.Name,
                p.SKU,
                p.DefaultSalePrice,
                p.IsActive))
            .ToListAsync(ct);

        return Result<List<ProductBriefDto>>.Success(products);
    }
}
