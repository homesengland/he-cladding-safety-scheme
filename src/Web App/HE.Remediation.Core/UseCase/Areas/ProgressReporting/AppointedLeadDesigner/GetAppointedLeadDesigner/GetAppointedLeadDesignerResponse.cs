
namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedLeadDesigner.GetAppointedLeadDesigner;

public class GetAppointedLeadDesignerResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool? LeadDesignerAppointed { get; set; }

    public bool? LeaseholdersInformed { get; set; }
}
