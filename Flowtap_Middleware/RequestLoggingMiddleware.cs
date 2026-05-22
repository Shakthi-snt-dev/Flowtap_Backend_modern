using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Flowtap_Middleware;

public class RequestLoggingMiddleware(
    RequestDelegate next,
    ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();

        var tenantId = context.User.FindFirst("tenantId")?.Value ?? "-";
        var method = context.Request.Method;
        var path = context.Request.Path;

        try
        {
            await next(context);
        }
        finally
        {
            sw.Stop();
            var statusCode = context.Response.StatusCode;

            logger.LogInformation(
                "{Method} {Path} → {StatusCode} | {Elapsed}ms | Tenant: {TenantId}",
                method, path, statusCode, sw.ElapsedMilliseconds, tenantId);
        }
    }
}
