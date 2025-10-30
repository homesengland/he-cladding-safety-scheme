using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;
namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class PracticalCompletionDateViewModel : PaymentRequestBaseViewModel
{
    public int? ExpectedPracticalDateMonth { get; set; }
    public int? ExpectedPracticalDateYear { get; set; }

    public DateTime? PreviousExpectedPracticalDate { get; set; }
}