using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetBankAccountDetailsRepresentative
{
    public class GetBankAccountDetailsRepresentativeRequest : IRequest<GetBankAccountDetailsRepresentativeResponse>
    {
        private GetBankAccountDetailsRepresentativeRequest()
        {

        }

        public static readonly GetBankAccountDetailsRepresentativeRequest Request = new();
    }
}