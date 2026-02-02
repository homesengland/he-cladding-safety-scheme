using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressSupport.SetProgressSupport;

public class SetProgressSupportRequest : IRequest
{
    public IList<EProgressReportSupportType> SupportTypes { get; set; } = new List<EProgressReportSupportType>();
    public string SupportNeededReason { get; set; }
}