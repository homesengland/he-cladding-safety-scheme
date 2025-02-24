using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CladdingSystem.Set;

public class SetCladdingSystemRequest : IRequest<Unit>
{
    public Guid FireRiskCladdingSystemsId { get; set; }

    public EReplacementCladding? IsBeingRemoved { get; set; }
}
