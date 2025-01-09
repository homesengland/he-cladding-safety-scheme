using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.FireRiskAppraisalToExternalWalls.Set;

public class SetFireRiskAppraisalToExternalWallsRequest : IRequest<Unit>
{
    private SetFireRiskAppraisalToExternalWallsRequest()
    {
    }

    public static readonly SetFireRiskAppraisalToExternalWallsRequest Request = new();
}
