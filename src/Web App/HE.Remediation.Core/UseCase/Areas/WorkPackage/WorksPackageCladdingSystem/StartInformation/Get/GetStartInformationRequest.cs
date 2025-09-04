using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.StartInformation.Get
{
    public class GetStartInformationRequest : IRequest<GetStartInformationResponse>
    {
        private GetStartInformationRequest()
        {
        }

        public static GetStartInformationRequest Request => new();
    }

}
