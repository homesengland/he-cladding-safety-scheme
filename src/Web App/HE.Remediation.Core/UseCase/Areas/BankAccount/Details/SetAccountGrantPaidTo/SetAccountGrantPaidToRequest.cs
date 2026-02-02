using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetAccountGrantPaidTo
{
    public class SetAccountGrantPaidToRequest : IRequest
    {
        public EBankDetailsRelationship BankDetailsRelationship { get; set; }
    }
}
