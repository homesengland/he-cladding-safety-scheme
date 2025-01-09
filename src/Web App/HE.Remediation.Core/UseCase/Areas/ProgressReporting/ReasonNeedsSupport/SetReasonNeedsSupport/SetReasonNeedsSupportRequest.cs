
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNeedsSupport.SetReasonNeedsSupport;

public class SetReasonNeedsSupportRequest : IRequest
{
    public string SupportNeededReason { get; set; }
}
