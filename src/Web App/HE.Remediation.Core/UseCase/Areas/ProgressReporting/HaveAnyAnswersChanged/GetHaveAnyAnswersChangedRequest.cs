using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.HaveAnyAnswersChanged;

public class GetHaveAnyAnswersChangedRequest : IRequest<GetHaveAnyAnswersChangedResponse>
{
    private GetHaveAnyAnswersChangedRequest()
    {
    }

    public static readonly GetHaveAnyAnswersChangedRequest Request = new();
}