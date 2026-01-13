using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;

public class CheckYourAnswersViewModel
{
    public EPursuedSourcesFundingType OtherSourcesPursuedTypeId { get; set; }
    public string OtherSourcesPursuedType { get; set; }
    public string CostRecoveryType { get; set; }
    public string OtherPartyPursuedRole { get; set; }

    public IList<string> FundingRouteTypes { get; set; } = new List<string>();
    public IList<PartyPursuedRoleViewModel> PartyPursuedRoles { get; set; } = new List<PartyPursuedRoleViewModel>();

    public bool IsSocialSector { get; set; }
    public bool ReadOnly { get; set; }
    public EApplicationScheme ApplicationScheme { get; set; }
    public class PartyPursuedRoleViewModel
    {
        public EPartyPursuedRole Id { get; set; }
        public string Name { get; set; }
    }
}