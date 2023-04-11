using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddInternalWallWorks;

public class SetAddInternalWallWorksRequest: IRequest<Unit>
{
    public Guid? Id { get; set; }  

    public string Description { get; set; }
}
