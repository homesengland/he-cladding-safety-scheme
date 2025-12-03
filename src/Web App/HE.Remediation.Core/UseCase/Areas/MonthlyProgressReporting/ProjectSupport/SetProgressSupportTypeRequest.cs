using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectSupport;

public class SetProgressSupportTypeRequest : IRequest
{
    public IList<EProgressReportSupportType> SupportTypes { get; set; } = new List<EProgressReportSupportType>();
    public string SupportNeededReason { get; set; }
    public ESubmitAction SubmitAction { get; set; }
    public ETaskStatus? TaskStatusId { get; set; }
}
