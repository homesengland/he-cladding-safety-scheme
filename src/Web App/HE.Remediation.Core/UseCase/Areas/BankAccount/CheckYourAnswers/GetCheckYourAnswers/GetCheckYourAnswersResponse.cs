using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.CheckYourAnswers.GetCheckYourAnswers;

public class GetCheckYourAnswersResponse
{
    public EResponsibleEntityRepresentationType RepresentationType { get; set; }
    public EBankDetailsRelationship BankDetailsRelationship { get; set; }
    public string NameOnTheAccount { get; set; }
    public string BankName { get; set; }
    public string BranchName { get; set; }
    public string AccountNumber { get; set; }
    public string SortCode { get; set; }
    public string VatNumber { get; set; }
    public string VerificationContactName { get; set; }
    public string VerificationContactNumber { get; set; }
    public bool ReadOnly { get; set; }
}