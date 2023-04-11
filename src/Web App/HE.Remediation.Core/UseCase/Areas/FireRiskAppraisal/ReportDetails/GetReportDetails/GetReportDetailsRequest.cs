
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ReportDetails.GetReportDetails;

public class GetReportDetailsRequest: IRequest<GetReportDetailsResponse>
{
    private GetReportDetailsRequest()
    {
    }

    public static readonly GetReportDetailsRequest Request = new();
}
