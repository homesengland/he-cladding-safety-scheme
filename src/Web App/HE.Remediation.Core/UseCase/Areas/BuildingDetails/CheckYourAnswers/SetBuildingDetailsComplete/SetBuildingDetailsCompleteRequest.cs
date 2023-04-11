using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.CheckYourAnswers.SetBuildingDetailsComplete
{
    public class SetBuildingDetailsCompleteRequest : IRequest<Unit>
    {
        private SetBuildingDetailsCompleteRequest() { }

        public static readonly SetBuildingDetailsCompleteRequest Request = new();
    }
}
