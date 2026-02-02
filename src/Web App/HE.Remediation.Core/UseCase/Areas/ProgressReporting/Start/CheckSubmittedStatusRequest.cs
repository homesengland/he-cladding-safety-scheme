using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Start;

public class CheckSubmittedStatusRequest : IRequest<bool>
{
    public Guid ProgressReportId { get; set; }
}