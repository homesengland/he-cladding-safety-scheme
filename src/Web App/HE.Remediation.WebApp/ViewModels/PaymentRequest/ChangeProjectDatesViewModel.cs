using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ChangeProjectDatesViewModel : PaymentRequestBaseViewModel
{
    public int? ProjectDateEndMonth { get; set; }
    public int? ProjectDateEndYear { get; set; }        
    public DateTime? ExpectedStartDate { get; set; }
}
