using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.GetReasonForClosing
{
    public class GetReasonForClosingRequest : IRequest<GetReasonForClosingResponse>
    {
        private GetReasonForClosingRequest()
        {
        }

        public static readonly GetReasonForClosingRequest Request = new();
    }
}
