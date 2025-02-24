using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.RequiresSubcontractors.Get;

public class GetRequiresSubcontractorsResponse
{
    public ENoYes? RequiresSubcontractors { get; set; }
    
    public ENoYes? SoughtQuotes { get; set; }
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }
}
