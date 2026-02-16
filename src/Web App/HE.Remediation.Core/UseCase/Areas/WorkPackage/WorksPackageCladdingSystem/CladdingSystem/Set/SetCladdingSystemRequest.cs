using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorksPackageCladdingSystem.CladdingSystem.Set;

public class SetCladdingSystemRequest : IRequest<Unit>
{
    public Guid FireRiskCladdingSystemsId { get; set; }

    public EReplacementCladding? IsBeingRemoved { get; set; }
}
