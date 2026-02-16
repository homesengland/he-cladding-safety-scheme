using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressReportVersion;

public class GetProgressReportVersionRequest : IRequest<int>
{
    private GetProgressReportVersionRequest()
    {
    }

    public static readonly GetProgressReportVersionRequest Request = new();
}