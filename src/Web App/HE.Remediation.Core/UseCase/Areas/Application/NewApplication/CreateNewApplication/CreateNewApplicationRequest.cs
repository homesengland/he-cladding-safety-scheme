using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.NewApplication.CreateNewApplication
{
    public class CreateNewApplicationRequest : IRequest<Unit>
    {
        public EApplicationScheme ApplicationScheme { get; set; }
        private CreateNewApplicationRequest()
        {

        }

        public static CreateNewApplicationRequest Request = new();
    }
}
