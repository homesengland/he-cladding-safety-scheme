using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Subcontractor.Set;

public class SetCostsSchedulingSubcontractorRequest : IRequest<Guid>
{
    public Guid? SubcontractorId { get; set; }
    public string CompanyName { get; set; }
    public string CompanyRegistration { get; set; }
}