using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.CheckYourAnswers.GetCheckYourAnswers;

public class GetCheckYourAnswersResponse
{
    public EPursuedSourcesFundingType OtherSourcesPursuedTypeId { get; set; }
    public string OtherSourcesPursuedType { get; set; }
    public string CostRecoveryType { get; set; }
    public string OtherPartyPursuedRole { get; set; }

    public IList<string> FundingRouteTypes { get; set; } = new List<string>();
    public IList<GetFundingRoutesCheckYourAnswersResult.PartyPursuedRole> PartyPursuedRoles { get; set; } = new List<GetFundingRoutesCheckYourAnswersResult.PartyPursuedRole>();

    public bool IsSocialSector { get; set; }
    public bool ReadOnly { get; set; }
}