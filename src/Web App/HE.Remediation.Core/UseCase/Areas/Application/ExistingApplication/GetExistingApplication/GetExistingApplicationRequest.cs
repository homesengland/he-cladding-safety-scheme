using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.ExistingApplication.GetExistingApplication
{
    public class GetExistingApplicationRequest : IRequest<List<GetExistingApplicationResponse>>
    {
        private GetExistingApplicationRequest()
        {
        }

        public static GetExistingApplicationRequest Request = new();
    }
}
