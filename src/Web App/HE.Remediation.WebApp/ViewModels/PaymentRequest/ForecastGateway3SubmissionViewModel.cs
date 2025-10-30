using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;
namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ForecastGateway3SubmissionViewModel : PaymentRequestBaseViewModel
{
    public int? ExpectedSubmissionDateMonth { get; set; }
    public int? ExpectedSubmissionDateYear { get; set; }

    public DateTime? PreviousExpectedSubmissionDate { get; set; }
}