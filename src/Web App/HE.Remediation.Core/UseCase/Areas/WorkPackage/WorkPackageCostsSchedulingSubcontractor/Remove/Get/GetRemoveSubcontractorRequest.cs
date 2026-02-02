using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Remove.Get;

public class GetRemoveSubcontractorRequest : IRequest<GetRemoveSubcontractorResponse>
{
    private GetRemoveSubcontractorRequest()
    {
    }
    
    public static GetRemoveSubcontractorRequest Request => new();
}
