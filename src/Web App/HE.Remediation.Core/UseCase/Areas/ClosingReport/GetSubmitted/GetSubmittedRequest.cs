using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubmitted;

public class GetSubmittedRequest : IRequest<GetSubmittedResponse>
{
    private GetSubmittedRequest()
    {
    }

    public static readonly GetSubmittedRequest Request = new();
}
