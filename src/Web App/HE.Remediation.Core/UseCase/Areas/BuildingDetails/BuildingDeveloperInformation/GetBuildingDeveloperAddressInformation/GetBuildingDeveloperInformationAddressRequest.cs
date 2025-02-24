using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.GetBuildingDeveloperInformation;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.GetBuildingDeveloperAddressInformation;

public class GetBuildingDeveloperInformationAddressRequest : IRequest<GetBuildingDeveloperInformationAddressResponse>
{
    private GetBuildingDeveloperInformationAddressRequest()
    {
    }

    public static readonly GetBuildingDeveloperInformationAddressRequest Request = new();
}
