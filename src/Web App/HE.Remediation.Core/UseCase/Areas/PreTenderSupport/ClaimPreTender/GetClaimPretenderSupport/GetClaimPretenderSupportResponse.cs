using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ClaimPreTender.GetClaimPretenderSupport;

public class GetClaimPretenderSupportResponse
{
    public EBankDetailsRelationship? bankRelationship { get; set; }

    public decimal? PTFSClaimAmount { get; set; }
}
