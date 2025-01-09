using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoDesigner.GetReasonNoDesigner;

public class GetReasonNoDesignerRequest : IRequest<GetReasonNoDesignerResponse>
{
    private GetReasonNoDesignerRequest()
    {
    }

    public static readonly GetReasonNoDesignerRequest Request = new();
}
