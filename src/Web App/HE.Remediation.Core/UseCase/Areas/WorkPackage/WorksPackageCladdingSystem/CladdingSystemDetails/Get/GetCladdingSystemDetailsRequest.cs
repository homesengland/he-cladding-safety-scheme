using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemDetails.Get;

public class GetCladdingSystemDetailsRequest : IRequest<GetCladdingSystemDetailsResponse>
{
    public Guid FireRiskCladdingSystemsId { get; set; }

    public int CladdingSystemIndex { get; set; }
}
