using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BankAccount
{
    public class AccountGrantPaidToViewModel
    {
        public Guid ApplicationId { get; set; }
        public EBankDetailsRelationship BankDetailsRelationship { get; set; }
        public ESubmitAction SubmitAction { get; set; }
    }
}