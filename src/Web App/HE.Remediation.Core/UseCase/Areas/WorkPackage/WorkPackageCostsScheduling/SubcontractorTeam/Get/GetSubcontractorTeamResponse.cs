using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ProjectTeam;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.SubcontractorTeam.Get;

public class GetSubcontractorTeamResponse
{
    public List<CostsScheduleSubcontractorResult> Subcontractors { get; set; }
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }
}
