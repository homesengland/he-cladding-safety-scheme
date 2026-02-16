using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNeedsSupport.GetReasonNeedsSupport;

public class GetReasonNeedsSupportRequest : IRequest<GetReasonNeedsSupportResponse>
{
    private GetReasonNeedsSupportRequest()
    {
    }

    public static readonly GetReasonNeedsSupportRequest Request = new();
}
