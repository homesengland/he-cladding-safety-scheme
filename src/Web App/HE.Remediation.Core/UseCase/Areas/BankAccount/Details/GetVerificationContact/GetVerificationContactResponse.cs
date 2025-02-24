using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetVerificationContact;

public class GetVerificationContactResponse
{
    public EBankDetailsRelationship BankDetailsRelationship { get; set; }
    public string ContactName { get; set; }
    public string ContactNumber { get; set; }
}