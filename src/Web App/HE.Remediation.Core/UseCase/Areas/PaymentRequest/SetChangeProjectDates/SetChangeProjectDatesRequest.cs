using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetChangeProjectDates;

public class SetChangeProjectDatesRequest : IRequest<SetChangeProjectDatesResponse>
{
    public int? ProjectDateEndMonth { get; set; }
    public int? ProjectDateEndYear { get; set; }    
    public DateTime? ExpectedStartDate { get; set; }
}
