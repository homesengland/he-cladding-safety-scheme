using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Subcontractor.Get;

public class GetCostsSchedulingSubcontractorRequest : IRequest<GetCostsSchedulingSubcontractorResponse>
{
    public Guid? SubcontractorId { get; set; }
}