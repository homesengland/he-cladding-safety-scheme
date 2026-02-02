using Mediator;
namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.ConfirmToProceed.GetConfirmToProceed
{
    public class GetWorkPackageConfirmToProceedRequest : IRequest<GetWorkPackageConfirmToProceedResponse>
    {
        private GetWorkPackageConfirmToProceedRequest()
        {
        }

        public static readonly GetWorkPackageConfirmToProceedRequest Request = new();
    }
}
