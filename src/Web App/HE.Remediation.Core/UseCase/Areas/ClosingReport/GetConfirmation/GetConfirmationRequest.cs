using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetConfirmation;

public class GetConfirmationRequest : IRequest<GetConfirmationResponse>
{
    private GetConfirmationRequest()
    {
    }

    public static readonly GetConfirmationRequest Request = new();
}
