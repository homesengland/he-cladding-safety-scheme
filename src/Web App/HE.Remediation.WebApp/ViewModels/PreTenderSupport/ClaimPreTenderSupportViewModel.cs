using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.PreTenderSupport;

public class ClaimPreTenderSupportViewModel
{
    public EBankDetailsRelationship? bankRelationship { get; set; }

    public decimal PTFSClaimAmount { get; set; }
}
