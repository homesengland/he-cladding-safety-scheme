using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetFundingRoutesCheckYourAnswersResult
{
    public Guid Id { get; set; }
    public EPursuedSourcesFundingType OtherSourcesPursuedTypeId { get; set; }
    public string OtherSourcesPursuedType { get; set; }
    public string CostRecoveryType { get; set; }
    public string OtherPartyPursuedRole { get; set; }

    public IList<FundingRouteType> FundingRouteTypes { get; set; } = new List<FundingRouteType>();
    public IList<PartyPursuedRole> PartyPursuedRoles { get; set; } = new List<PartyPursuedRole>();

    public class FundingRouteType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class PartyPursuedRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}