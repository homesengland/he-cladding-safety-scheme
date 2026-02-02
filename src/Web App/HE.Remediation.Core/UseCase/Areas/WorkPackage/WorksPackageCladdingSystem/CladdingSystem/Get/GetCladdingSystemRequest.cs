using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorksPackageCladdingSystem.CladdingSystem.Get;

public class GetCladdingSystemRequest : IRequest<GetCladdingSystemResponse>
{
    public Guid FireRiskCladdingSystemsId { get; set; }

    public int CladdingSystemIndex { get; set; }
}
