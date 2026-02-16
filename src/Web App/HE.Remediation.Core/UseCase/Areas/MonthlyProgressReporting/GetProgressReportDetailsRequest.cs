using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting;

public class GetProgressReportDetailsRequest : IRequest<GetProgressReportDetailsResponse>
{
    public Guid ProgressReportId { get; set; }
}
