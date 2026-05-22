using Flowtap_Domain.BoundedContexts.Core.Identity.Entities;
using Flowtap_Domain.BoundedContexts.Core.Organization.Entities;

using Flowtap_Domain.BoundedContexts.Modules.Inventory.Entities;
using Flowtap_Domain.BoundedContexts.Modules.Purchase.Entities;
using Flowtap_Domain.BoundedContexts.Modules.Sales.Entities;
using Flowtap_Domain.BoundedContexts.Modules.ServiceTickets.Entities;
using Flowtap_Domain.BoundedContexts.Modules.Subscription.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flowtap_Application.Common.Interfaces;

public interface IApplicationDbContext
{
    // Identity
    DbSet<UserAccount> UserAccounts { get; }
    DbSet<UserProfile> UserProfiles { get; }
    DbSet<UserSession> UserSessions { get; }
    DbSet<UserNotificationSettings> UserNotificationSettings { get; }
    DbSet<RefreshToken> RefreshTokens { get; }

    // Organization
    DbSet<Tenant> Tenants { get; }
    DbSet<TenantSettings> TenantSettings { get; }
    DbSet<Store> Stores { get; }
    DbSet<Employee> Employees { get; }
    DbSet<SalarySetting> SalarySettings { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<PermissionCategory> PermissionCategories { get; }
    DbSet<StatusPermission> StatusPermissions { get; }
    DbSet<EmployeePermission> EmployeePermissions { get; }
    DbSet<EmployeeStatusPermission> EmployeeStatusPermissions { get; }
    DbSet<EmployeeLocationAccess> EmployeeLocationAccesses { get; }
    DbSet<EmployeeRights> EmployeeRights { get; }
    DbSet<StoreSetting> StoreSettings { get; }
    DbSet<OrderType> OrderTypes { get; }
    DbSet<LocationOrderType> LocationOrderTypes { get; }
    DbSet<TaxConfiguration> TaxConfigurations { get; }
    DbSet<TaxSlab> TaxSlabs { get; }
    DbSet<TaxRateRule> TaxRateRules { get; }
    DbSet<AppUser> AppUsers { get; }
    DbSet<NotificationQueue> NotificationQueues { get; }
    DbSet<AdminBroadcast> AdminBroadcasts { get; }
    DbSet<AnnouncementTarget> AnnouncementTargets { get; }
    DbSet<DirectMessage> DirectMessages { get; }
    DbSet<Conversation> Conversations { get; }
    DbSet<ConversationParticipant> ConversationParticipants { get; }
    DbSet<ChatMessage> ChatMessages { get; }
    DbSet<UserNotification> UserNotifications { get; }

    // Inventory
    DbSet<Product> Products { get; }
    DbSet<ProductCategory> ProductCategories { get; }
    DbSet<ProductVariant> ProductVariants { get; }
    DbSet<ProductMedia> ProductMedia { get; }
    DbSet<Warehouse> Warehouses { get; }
    DbSet<WarehouseStock> WarehouseStocks { get; }
    DbSet<WarehouseRack> WarehouseRacks { get; }
    DbSet<WarehouseBin> WarehouseBins { get; }
    DbSet<WarehouseBinStock> WarehouseBinStocks { get; }
    DbSet<InventoryTransaction> InventoryTransactions { get; }
    DbSet<InventoryCostLayer> InventoryCostLayers { get; }
    DbSet<InventorySerial> InventorySerials { get; }
    DbSet<InventorySerialLocation> InventorySerialLocations { get; }
    DbSet<InventoryTransfer> InventoryTransfers { get; }
    DbSet<InventoryTransferItem> InventoryTransferItems { get; }
    DbSet<InventoryWriteOff> InventoryWriteOffs { get; }
    DbSet<InventoryWriteOffAttachment> InventoryWriteOffAttachments { get; }
    DbSet<StockAdjustment> StockAdjustments { get; }
    DbSet<StockBatch> StockBatches { get; }
    DbSet<ReorderRule> ReorderRules { get; }
    DbSet<ReorderAlert> ReorderAlerts { get; }
    DbSet<BarcodeTemplate> BarcodeTemplates { get; }
    DbSet<DeviceBrand> DeviceBrands { get; }
    DbSet<DeviceModel> DeviceModels { get; }
    DbSet<ProductDeviceModelMapping> ProductDeviceModelMappings { get; }
    DbSet<InventorySettings> InventorySettings { get; }
    DbSet<LocationInventorySettings> LocationInventorySettings { get; }
    DbSet<ProductLocationPrice> ProductLocationPrices { get; }
    DbSet<SerialCounter> SerialCounters { get; }

    // Sales
    DbSet<Client> Clients { get; }
    DbSet<Sale> Sales { get; }
    DbSet<SaleItem> SaleItems { get; }
    DbSet<SaleHistory> SaleHistories { get; }
    DbSet<Payment> Payments { get; }
    DbSet<PaymentAccount> PaymentAccounts { get; }
    DbSet<PaymentMethodMapping> PaymentMethodMappings { get; }
    DbSet<PaymentGatewayTransaction> PaymentGatewayTransactions { get; }
    DbSet<Campaign> Campaigns { get; }
    DbSet<CampaignProductRule> CampaignProductRules { get; }
    DbSet<MarketingCampaign> MarketingCampaigns { get; }
    DbSet<CampaignTargetLocation> CampaignTargetLocations { get; }
    DbSet<Offer> Offers { get; }

    // Service Tickets
    DbSet<ServiceTicket> ServiceTickets { get; }
    DbSet<ServiceTicketItem> ServiceTicketItems { get; }
    DbSet<ServiceTicketPartUsage> ServiceTicketPartUsages { get; }
    DbSet<TicketTimeLog> TicketTimeLogs { get; }
    DbSet<Service> Services { get; }
    DbSet<ServiceCategory> ServiceCategories { get; }
    DbSet<ServicePartRequirement> ServicePartRequirements { get; }
    DbSet<ServiceDeviceModelMapping> ServiceDeviceModelMappings { get; }
    DbSet<TechnicalFault> TechnicalFaults { get; }
    DbSet<RepairChecklistTemplate> RepairChecklistTemplates { get; }
    DbSet<WorkTask> WorkTasks { get; }
    DbSet<WorkTaskTag> WorkTaskTags { get; }
    DbSet<TaskTimeLog> TaskTimeLogs { get; }
    DbSet<ActivityLog> ActivityLogs { get; }

    // Purchase
    DbSet<Supplier> Suppliers { get; }
    DbSet<PurchaseOrder> PurchaseOrders { get; }
    DbSet<PurchaseOrderItem> PurchaseOrderItems { get; }
    DbSet<PurchaseReturn> PurchaseReturns { get; }
    DbSet<PurchaseReturnItem> PurchaseReturnItems { get; }

    // Integrations
    DbSet<Integration> Integrations { get; }

    // Subscription
    DbSet<SubscriptionPlan> SubscriptionPlans { get; }
    DbSet<CompanySubscription> CompanySubscriptions { get; }
    DbSet<SubscriptionLocation> SubscriptionLocations { get; }
    DbSet<BillingInvoice> BillingInvoices { get; }
    DbSet<BillingPayment> BillingPayments { get; }
    DbSet<SubscriptionChangeLog> SubscriptionChangeLogs { get; }
    DbSet<TrialPlan> TrialPlans { get; }

   

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
