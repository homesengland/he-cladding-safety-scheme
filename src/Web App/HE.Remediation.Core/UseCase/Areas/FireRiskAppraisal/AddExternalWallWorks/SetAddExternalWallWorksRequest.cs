
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddExternalWallWorks;

public class SetAddExternalWallWorksRequest: IRequest<Unit>
{
    public Guid? Id { get; set; }  

    public string Description { get; set; }
}
