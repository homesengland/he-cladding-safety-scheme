using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressSupport.GetProgressSupport;

public class GetProgressSupportRequest : IRequest<GetProgressSupportResponse>
{
    private GetProgressSupportRequest()
    {
    }

    public static readonly GetProgressSupportRequest Request = new();
}