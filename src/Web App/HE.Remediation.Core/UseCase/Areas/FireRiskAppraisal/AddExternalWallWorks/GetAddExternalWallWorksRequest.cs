using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddExternalWallWorks;

public class GetAddExternalWallWorksRequest: IRequest<GetAddExternalWallWorksResponse>
{
    public Guid? Id { get; set; }    
}
