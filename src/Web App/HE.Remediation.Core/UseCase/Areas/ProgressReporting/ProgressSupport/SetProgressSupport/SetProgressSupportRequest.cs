using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressSupport.SetProgressSupport;

public class SetProgressSupportRequest : IRequest
{
    public IList<EProgressReportSupportType> SupportTypes { get; set; } = new List<EProgressReportSupportType>();
    public string SupportNeededReason { get; set; }
}