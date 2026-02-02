using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.ResetCladdingSystem;

public class ResetCladdingSystemRequest : IRequest 
{
    public Guid FireRiskCladdingSystemsId { get; internal set; }
}