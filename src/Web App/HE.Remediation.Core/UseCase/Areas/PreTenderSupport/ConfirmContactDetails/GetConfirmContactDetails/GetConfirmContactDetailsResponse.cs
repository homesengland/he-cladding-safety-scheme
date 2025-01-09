
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ConfirmContactDetails.GetConfirmContactDetails;

public class GetConfirmContactDetailsResponse
{
    public List<GetSignatoryResult> Signatures { get; set; }
    public EBankDetailsRelationship BankDetailsRelationship { get; set; }
}
