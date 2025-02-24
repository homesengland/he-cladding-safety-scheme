using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BankAccount;

public class VerificationContactViewModel
{
    public EBankDetailsRelationship BankDetailsRelationship { get; set; }

    public string ContactName { get; set; }
    public string ContactNumber { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}