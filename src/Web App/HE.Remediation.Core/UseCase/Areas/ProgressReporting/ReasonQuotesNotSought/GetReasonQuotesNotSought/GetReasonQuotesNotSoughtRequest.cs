using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonQuotesNotSought.GetReasonQuotesNotSought;

public class GetReasonQuotesNotSoughtRequest : IRequest<GetReasonQuotesNotSoughtResponse>
{
    private GetReasonQuotesNotSoughtRequest()
    {
    }

    public static readonly GetReasonQuotesNotSoughtRequest Request = new();
}
