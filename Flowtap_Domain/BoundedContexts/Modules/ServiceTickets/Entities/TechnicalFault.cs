using Flowtap_Domain.SharedKernel;
namespace Flowtap_Domain.BoundedContexts.Modules.ServiceTickets.Entities;
public class TechnicalFault : TenantEntity
{
    public string Symptom { get; set; } = string.Empty;
    public string? PossibleCause { get; set; }
    public string? StandardSolution { get; set; }
    public bool IsActive { get; set; } = true;
}
