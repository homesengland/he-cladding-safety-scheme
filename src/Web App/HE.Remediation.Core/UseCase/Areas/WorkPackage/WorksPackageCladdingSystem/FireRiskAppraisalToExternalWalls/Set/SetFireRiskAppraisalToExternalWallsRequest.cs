using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.FireRiskAppraisalToExternalWalls.Set;

public class SetFireRiskAppraisalToExternalWallsRequest : IRequest<Unit>
{
    private SetFireRiskAppraisalToExternalWallsRequest()
    {
    }

    public static readonly SetFireRiskAppraisalToExternalWallsRequest Request = new();
}
