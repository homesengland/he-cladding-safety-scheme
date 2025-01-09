
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoDesigner.SetReasonNoDesigner;

public class SetReasonNoDesignerRequest : IRequest
{
    public string LeadDesignerNotAppointedReason { get; set; }

    public bool? LeadDesignerNeedsSupport { get; set; }
}
