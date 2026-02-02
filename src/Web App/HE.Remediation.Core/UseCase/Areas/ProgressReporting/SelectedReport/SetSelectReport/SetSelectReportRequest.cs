
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.SelectedReport.SetSelectReport;

public class SetSelectReportRequest : IRequest
{
    public Guid? Id { get; set; }
}
