using Flowtap_Application.Common.DTOs;
using Flowtap_Application.Common.Interfaces;
using Flowtap_Application.Features.Organization.Tenant.Commands.SeedIndustryData.Seeders;
using Flowtap_Domain.BoundedContexts.Core.Organization.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Flowtap_Application.Features.Organization.Tenant.Commands.SeedIndustryData;

public class SeedIndustryDataCommandHandler(IApplicationDbContext context)
    : IRequestHandler<SeedIndustryDataCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(SeedIndustryDataCommand command, CancellationToken ct)
    {
        var tenant = await context.Tenants.FirstOrDefaultAsync(t => t.Id == command.CompanyId, ct);
        if (tenant == null)
            return Result<bool>.Failure("Tenant not found");

        if (!Enum.TryParse<IndustryType>(command.IndustryType, true, out var industryType))
            return Result<bool>.Failure($"Invalid industry type: {command.IndustryType}");

        // Set active modules
        tenant.ActiveModules = industryType switch
        {
            IndustryType.RepairShop => "POS,Inventory,ServiceTickets,Purchasing,Devices",
            IndustryType.Restaurant => "POS,Inventory,KOT,Tables,Recipes,Purchasing",
            IndustryType.Hotel => "POS,Rooms,Bookings,HouseKeeping,Inventory",
            IndustryType.Jewelry => "POS,Inventory,JewelryItems,MetalRates,MetalExchange,Purchasing",
            IndustryType.Medical => "Patients,Appointments,Consultations,Pharmacy,Inventory",
            _ => "POS,Inventory,Purchasing,Customers"
        };

        // Seed based on industry + business type
        switch (industryType)
        {
            case IndustryType.RepairShop:
                var businessTypeLower = command.BusinessType.ToLower();
                if (businessTypeLower.Contains("mobile") || businessTypeLower.Contains("phone"))
                    await RepairShopMobileSeeder.SeedAsync(context, command.CompanyId, ct);
                else if (businessTypeLower.Contains("car") || businessTypeLower.Contains("auto"))
                    await RepairShopCarSeeder.SeedAsync(context, command.CompanyId, ct);
                else if (businessTypeLower.Contains("laptop") || businessTypeLower.Contains("computer"))
                    await RepairShopLaptopSeeder.SeedAsync(context, command.CompanyId, ct);
                else
                    await RepairShopMobileSeeder.SeedAsync(context, command.CompanyId, ct); // default
                break;
            case IndustryType.Restaurant:
                await RestaurantSeeder.SeedAsync(context, command.CompanyId, ct);
                break;
            case IndustryType.Hotel:
                await HotelSeeder.SeedAsync(context, command.CompanyId, ct);
                break;
            case IndustryType.Jewelry:
                await JewellerySeeder.SeedAsync(context, command.CompanyId, ct);
                break;
            case IndustryType.Medical:
                await MedicalSeeder.SeedAsync(context, command.CompanyId, ct);
                break;
            default:
                await RetailSeeder.SeedAsync(context, command.CompanyId, ct);
                break;
        }

        await context.SaveChangesAsync(ct);
        return Result<bool>.Success(true);
    }
}
