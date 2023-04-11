using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InternalWallWorks;

public class SetDeleteInternalWallWorksRequest: IRequest
{
    public Guid Id { get; set; }

}
