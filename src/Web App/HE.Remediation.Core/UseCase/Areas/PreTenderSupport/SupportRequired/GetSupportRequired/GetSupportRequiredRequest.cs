using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.SupportRequired.GetSupportRequired
{
    public class GetSupportRequiredRequest : IRequest<GetSupportRequiredResponse>
    {
        private GetSupportRequiredRequest()
        {

        }

        public static readonly GetSupportRequiredRequest Request = new();
    }
}