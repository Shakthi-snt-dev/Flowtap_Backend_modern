using Flowtap_Domain.SharedKernel;
namespace Flowtap_Domain.BoundedContexts.Modules.ServiceTickets.Entities;
public class ServicePartRequirement : BaseEntity
{
    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
