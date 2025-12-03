using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.NoTeam;
public class ProjectTeamNoTeamViewModel
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string ReasonNoTeam { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}
