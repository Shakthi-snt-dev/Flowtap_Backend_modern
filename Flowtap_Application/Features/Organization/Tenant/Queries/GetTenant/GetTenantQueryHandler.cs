using AutoMapper;
using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Exceptions;
using Flowtap_Application.Common.Interfaces;
using Flowtap_Application.Features.Organization.Tenant.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Flowtap_Application.Features.Organization.Tenant.Queries.GetTenant;

public class GetTenantQueryHandler(IApplicationDbContext db, IMapper mapper)
    : IRequestHandler<GetTenantQuery, Result<TenantDto>>
{
    public async Task<Result<TenantDto>> Handle(GetTenantQuery request, CancellationToken ct)
    {
        var tenant = await db.Tenants
            .Include(t => t.Settings)
            .FirstOrDefaultAsync(t => t.Id == request.TenantId && t.IsActive, ct)
            ?? throw new NotFoundException(nameof(Flowtap_Domain.BoundedContexts.Core.Organization.Entities.Tenant), request.TenantId);

        return Result<TenantDto>.Success(new TenantDto(
            tenant.Id, tenant.Title, tenant.Phone, tenant.Email,
            tenant.Country, tenant.Currency, tenant.SubDomain, tenant.BusinessType,
            tenant.IndustryType.ToString(), tenant.IsActive,
            tenant.Settings?.IsOnboardingComplete ?? false,
            tenant.ActiveModules,
            tenant.Settings?.MaxLocations ?? 1,
            tenant.Settings?.MaxEmployees ?? 5,
            tenant.Settings?.TimeZoneId,
            tenant.Settings?.LogoUrl));
    }
}
