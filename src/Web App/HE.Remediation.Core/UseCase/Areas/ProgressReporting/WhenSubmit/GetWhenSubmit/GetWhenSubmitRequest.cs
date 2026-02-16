using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.GetWhenSubmit;

public class GetWhenSubmitRequest : IRequest<GetWhenSubmitResponse>
{
    private GetWhenSubmitRequest()
    {
    }

    public static readonly GetWhenSubmitRequest Request = new();
}
