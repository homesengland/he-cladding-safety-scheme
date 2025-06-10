
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.SetReasonForClosing
{
    public class SetReasonForClosingRequest : IRequest
    {

        public string ReasonForClosing { get; set; }
    }

}
