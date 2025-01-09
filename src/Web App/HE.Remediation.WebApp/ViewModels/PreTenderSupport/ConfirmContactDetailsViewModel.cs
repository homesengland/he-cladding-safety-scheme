using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.PreTenderSupport;

public class ConfirmContactDetailsViewModel
{
    public List<GetSignatoryResult> Signatures { get; set; }
    public EBankDetailsRelationship BankDetailsRelationship { get; set; }
}
