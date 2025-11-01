
namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetPracticalCompletionDate
{
    public class GetPracticalCompletionDateResponse
    {
        public bool IsFirstPaymentRequest { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }

        public bool IsSubmitted { get; set; }

        public bool IsExpired { get; set; }

        public int? ExpectedPracticalDateMonth { get; set; }
        public int? ExpectedPracticalDateYear { get; set; }

        public DateTime? PreviousExpectedPracticalDate { get; set; }
    }
}
