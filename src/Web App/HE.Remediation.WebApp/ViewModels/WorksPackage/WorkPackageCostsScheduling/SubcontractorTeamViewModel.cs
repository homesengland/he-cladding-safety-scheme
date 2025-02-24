using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ProjectTeam;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class SubcontractorTeamViewModel : WorkPackageBaseViewModel
{
    public List<CostsScheduleSubcontractorResult> Subcontractors { get; set; }
}