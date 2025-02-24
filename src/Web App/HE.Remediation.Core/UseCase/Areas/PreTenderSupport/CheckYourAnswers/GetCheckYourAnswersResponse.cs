
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.CheckYourAnswers;

public class GetCheckYourAnswersResponse
{
    public bool? SupportRequired { get; set; }
    
    public List<GetSignatoryResult> Signatures { get; set; }

    public EBankDetailsRelationship BankDetailsRelationship { get; set; }
}

