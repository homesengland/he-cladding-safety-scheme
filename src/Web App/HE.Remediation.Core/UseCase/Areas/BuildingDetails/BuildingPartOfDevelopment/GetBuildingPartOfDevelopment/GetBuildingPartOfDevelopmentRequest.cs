using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.GetBuildingPartOfDevelopment
{
    public class GetBuildingPartOfDevelopmentRequest : IRequest<GetBuildingPartOfDevelopmentResponse>
    {
        private GetBuildingPartOfDevelopmentRequest() { }

        public static readonly GetBuildingPartOfDevelopmentRequest Request = new();
    }
}
