using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Interfaces;
using Flowtap_Application.Features.Reports.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Flowtap_Application.Features.Reports.Queries.GetDashboardStats;

public class GetDashboardStatsQueryHandler(IApplicationDbContext db)
    : IRequestHandler<GetDashboardStatsQuery, Result<DashboardStatsDto>>
{
    public async Task<Result<DashboardStatsDto>> Handle(GetDashboardStatsQuery request, CancellationToken ct)
    {
        var today = DateTime.UtcNow.Date;
        var monthStart = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);

        var salesQuery = db.Sales.Where(s => s.CompanyId == request.CompanyId);
        if (request.LocationId.HasValue)
            salesQuery = salesQuery.Where(s => s.LocationId == request.LocationId.Value);

        var revenueToday = await salesQuery
            .Where(s => s.CreatedAt >= today)
            .SumAsync(s => (decimal?)s.TotalAmount, ct) ?? 0;

        var transactionsToday = await salesQuery
            .CountAsync(s => s.CreatedAt >= today, ct);

        var revenueThisMonth = await salesQuery
            .Where(s => s.CreatedAt >= monthStart)
            .SumAsync(s => (decimal?)s.TotalAmount, ct) ?? 0;

        var newClients = await db.Clients
            .CountAsync(c => c.CompanyId == request.CompanyId && c.CreatedAt >= monthStart, ct);

        var openTickets = await db.ServiceTickets
            .CountAsync(t => t.CompanyId == request.CompanyId && t.ClosedAt == null, ct);

        var lowStockAlerts = await db.ReorderAlerts
            .CountAsync(a => a.CompanyId == request.CompanyId && !a.IsHandled, ct);

        var stats = new DashboardStatsDto(
            revenueToday, transactionsToday, newClients,
            openTickets, lowStockAlerts, revenueThisMonth);

        return Result<DashboardStatsDto>.Success(stats);
    }
}
