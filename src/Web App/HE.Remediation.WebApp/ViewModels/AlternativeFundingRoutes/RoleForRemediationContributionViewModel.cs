using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;

public class RoleForRemediationContributionViewModel
{
    public IList<EPartyPursuedRole> Roles { get; set; } = new List<EPartyPursuedRole>();
    public bool VisitedCheckYourAnswers { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}