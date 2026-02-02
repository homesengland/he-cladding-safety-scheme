using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetProjectDates;

public class GetProjectDatesRequest : IRequest<GetProjectDatesResponse>
{
    private GetProjectDatesRequest()
    {
    }

    public static readonly GetProjectDatesRequest Request = new();
}
