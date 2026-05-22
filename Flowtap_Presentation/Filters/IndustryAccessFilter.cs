using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Interfaces;
using Flowtap_Domain.BoundedContexts.Core.Organization.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Flowtap_Presentation.Filters;

public class IndustryAccessFilter(IndustryType[] requiredIndustries, IApplicationDbContext context) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext ctx, ActionExecutionDelegate next)
    {
        var tenantIdClaim = ctx.HttpContext.User.FindFirstValue("tenantId");
        if (!Guid.TryParse(tenantIdClaim, out var tenantId))
        {
            ctx.Result = new ForbidResult();
            return;
        }

        var tenant = await context.Tenants.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == tenantId);

        if (tenant == null || !requiredIndustries.Contains(tenant.IndustryType))
        {
            ctx.Result = new ObjectResult(ApiResponse<object>.Fail("This feature is not available for your industry type."))
            { StatusCode = 403 };
            return;
        }

        await next();
    }
}
