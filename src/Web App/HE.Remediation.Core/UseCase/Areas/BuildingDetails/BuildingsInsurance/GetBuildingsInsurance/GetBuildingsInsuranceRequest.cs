using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.GetBuildingsInsurance;

public class GetBuildingsInsuranceRequest : IRequest<GetBuildingsInsuranceResponse>
{
    private GetBuildingsInsuranceRequest()
    {
    }

    public static readonly GetBuildingsInsuranceRequest Request = new();
}