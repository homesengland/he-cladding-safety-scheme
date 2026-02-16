using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.InformedLeaseholder.GetInformedLeaseholder;

public class GetInformedLeaseholderRequest : IRequest<GetInformedLeaseholderResponse>
{
    private GetInformedLeaseholderRequest()
    {
    }

    public static readonly GetInformedLeaseholderRequest Request = new();
}
