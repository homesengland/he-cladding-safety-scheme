using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetBankAccountDetailsResponsibleEntity
{
    public class GetBankAccountDetailsResponsibleEntityRequest : IRequest<GetBankAccountDetailsResponsibleEntityResponse>
    {
        private GetBankAccountDetailsResponsibleEntityRequest()
        {

        }

        public static readonly GetBankAccountDetailsResponsibleEntityRequest Request = new();
    }
}