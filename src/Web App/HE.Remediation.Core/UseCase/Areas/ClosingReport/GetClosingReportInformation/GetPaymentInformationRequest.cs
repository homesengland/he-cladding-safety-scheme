using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetClosingReportInformation;

public class GetClosingReportInformationRequest : IRequest<GetClosingReportInformationResponse>
{
    private GetClosingReportInformationRequest()
    {
    }

    public static readonly GetClosingReportInformationRequest Request = new();
}
