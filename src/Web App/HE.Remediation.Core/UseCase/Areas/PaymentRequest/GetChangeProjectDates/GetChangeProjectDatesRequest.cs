using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetChangeProjectDates;

public class GetChangeProjectDatesRequest : IRequest<GetChangeProjectDatesResponse>
{
    private GetChangeProjectDatesRequest()
    {
    }

    public static readonly GetChangeProjectDatesRequest Request = new();
}
