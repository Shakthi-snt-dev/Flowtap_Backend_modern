using Flowtap_Domain.SharedKernel;
namespace Flowtap_Domain.BoundedContexts.Modules.ServiceTickets.Entities;
public class WorkTaskTag : BaseEntity
{
    public Guid WorkTaskId { get; set; }
    public WorkTask WorkTask { get; set; } = null!;
    public string Tag { get; set; } = string.Empty;
}
