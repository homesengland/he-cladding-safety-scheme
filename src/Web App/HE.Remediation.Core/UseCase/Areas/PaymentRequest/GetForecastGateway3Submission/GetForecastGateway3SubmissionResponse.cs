
namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetForecastGateway3Submission
{
    public class GetForecastGateway3SubmissionResponse
    {
        public bool IsFirstPaymentRequest { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }

        public bool IsSubmitted { get; set; }

        public bool IsExpired { get; set; }

        public int? ExpectedSubmissionDateMonth { get; set; }
        public int? ExpectedSubmissionDateYear { get; set; }

        public DateTime? PreviousExpectedSubmissionDate { get; set; }
    }
}
