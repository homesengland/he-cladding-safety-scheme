using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetProjectDates;

public class SetProjectDatesRequest : IRequest<SetProjectDatesResponse>
{
    public bool? ProjectDatesChanged { get; set; }

    public DateTime? ExpectedStartDate { get; set; }

    public DateTime? ExpectedEndDate { get; set; }

    public bool? UnsafeCladdingAlreadyRemoved { get; set; }
}
