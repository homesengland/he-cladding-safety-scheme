
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Remove.Set;

public class DeleteSubcontractorRequest : IRequest
{
    public Guid SubcontractorId { get; set; }
}
