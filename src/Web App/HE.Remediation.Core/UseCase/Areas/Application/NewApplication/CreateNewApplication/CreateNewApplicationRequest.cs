using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.NewApplication.CreateNewApplication
{
    public class CreateNewApplicationRequest : IRequest<Unit>
    {
        private CreateNewApplicationRequest()
        {

        }

        public static CreateNewApplicationRequest Request = new();
    }
}
