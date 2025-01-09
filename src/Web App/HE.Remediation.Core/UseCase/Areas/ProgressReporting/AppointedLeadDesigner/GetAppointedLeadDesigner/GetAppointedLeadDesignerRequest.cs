using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedLeadDesigner.GetAppointedLeadDesigner;

public class GetAppointedLeadDesignerRequest : IRequest<GetAppointedLeadDesignerResponse>
{
    private GetAppointedLeadDesignerRequest()
    {
    }

    public static readonly GetAppointedLeadDesignerRequest Request = new();
}
