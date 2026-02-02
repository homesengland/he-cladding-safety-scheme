using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.CheckYourAnswers.GetBuildingDetailsAnswers
{
    public class GetBuildingDetailsAnswersRequest : IRequest<GetBuildingDetailsAnswersResponse>
    {
        private GetBuildingDetailsAnswersRequest() { }

        public static readonly GetBuildingDetailsAnswersRequest Request = new();
    }
}