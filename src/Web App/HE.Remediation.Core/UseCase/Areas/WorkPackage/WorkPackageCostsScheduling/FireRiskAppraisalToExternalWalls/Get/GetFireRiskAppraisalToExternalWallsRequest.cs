using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.FireRiskAppraisalToExternalWalls.Get;

public class GetFireRiskAppraisalToExternalWallsRequest : IRequest<GetFireRiskAppraisalToExternalWallsResponse>
{
    private GetFireRiskAppraisalToExternalWallsRequest()
    {
    }

    public static GetFireRiskAppraisalToExternalWallsRequest Request => new();
}
