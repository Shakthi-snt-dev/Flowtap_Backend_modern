using Flowtap_Domain.SharedKernel;
namespace Flowtap_Domain.BoundedContexts.Modules.ServiceTickets.Entities;
public class TaskTimeLog : BaseEntity
{
    public Guid TaskId { get; set; }
    public WorkTask WorkTask { get; set; } = null!;
    public Guid EmployeeId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? StoppedAt { get; set; }
}
