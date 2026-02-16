using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressReports.GetProgressReports;

public class GetProgressReportsRequest : IRequest<GetProgressReportsResponse>
{
    private GetProgressReportsRequest()
    {
    }

    public static readonly GetProgressReportsRequest Request = new();
}
