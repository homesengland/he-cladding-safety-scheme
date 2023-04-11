using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddInternalWallWorks;

public class GetAddInternalWallWorksRequest: IRequest<GetAddInternalWallWorksResponse>
{
    public Guid? Id { get; set; }    
}
