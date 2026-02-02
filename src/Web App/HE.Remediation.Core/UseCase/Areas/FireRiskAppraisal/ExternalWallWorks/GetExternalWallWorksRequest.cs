
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWallWorks;

public class GetExternalWallWorksRequest: IRequest<List<GetWallWorksListResult>>
{
    public string Id { get; set; }

    public string Description { get; set; }

    private GetExternalWallWorksRequest()
    {
    }

    public static readonly GetExternalWallWorksRequest Request = new();
}
