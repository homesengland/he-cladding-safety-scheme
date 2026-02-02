using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.GetBuildingDeveloperInformation;

public class GetBuildingDeveloperInformationRequest : IRequest<GetBuildingDeveloperInformationResponse>
{
    private GetBuildingDeveloperInformationRequest()
    {
    }

    public static readonly GetBuildingDeveloperInformationRequest Request = new();
}