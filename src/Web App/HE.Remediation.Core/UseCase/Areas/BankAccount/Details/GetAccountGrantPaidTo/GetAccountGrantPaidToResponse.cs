using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetAccountGrantPaidTo
{
    public class GetAccountGrantPaidToResponse
    {
        public Guid ApplicationId { get; set; }
        public EBankDetailsRelationship BankDetailsRelationship { get; set; }
    }
}