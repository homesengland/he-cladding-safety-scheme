using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetClosingReportAbout;

public class GetClosingReportAboutRequest : IRequest<GetClosingReportAboutResponse>
{
    private GetClosingReportAboutRequest()
    {
    }

    public static readonly GetClosingReportAboutRequest Request = new();
}
