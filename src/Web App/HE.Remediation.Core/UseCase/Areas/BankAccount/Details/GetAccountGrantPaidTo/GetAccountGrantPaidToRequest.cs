using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetAccountGrantPaidTo
{
    public class GetAccountGrantPaidToRequest : IRequest<GetAccountGrantPaidToResponse>
    {
        private GetAccountGrantPaidToRequest()
        {

        }

        public static readonly GetAccountGrantPaidToRequest Request = new();
    }
}