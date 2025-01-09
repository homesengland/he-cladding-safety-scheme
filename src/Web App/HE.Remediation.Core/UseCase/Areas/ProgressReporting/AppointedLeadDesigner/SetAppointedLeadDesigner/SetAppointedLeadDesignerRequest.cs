
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedLeadDesigner.SetAppointedLeadDesigner;

public class SetAppointedLeadDesignerRequest : IRequest
{
    public bool? LeadDesignerAppointed { get; set; }
}
