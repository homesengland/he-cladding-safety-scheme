

using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InternalWallWorks;

public class GetInternalWallWorksRequest: IRequest<List<GetWallWorksListResult>>
{
    private GetInternalWallWorksRequest()
    {
    }

    public static readonly GetInternalWallWorksRequest Request = new();
}
