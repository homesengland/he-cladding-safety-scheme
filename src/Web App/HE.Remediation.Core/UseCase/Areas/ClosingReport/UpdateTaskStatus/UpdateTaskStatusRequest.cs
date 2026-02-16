using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.ProceedFromAbout;

public class UpdateTaskStatusRequest(EClosingReportTask closingReportTask, ETaskStatus status) : IRequest
{
    public EClosingReportTask ClosingReportTask { get; } = closingReportTask;
    public ETaskStatus TaskStatus { get; } = status;
    public bool AllowRevert { get; set; }
}
