
namespace HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.GetReasonForClosing
{
    public class GetReasonForClosingResponse
    {
        public string ReasonForClosing { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public bool IsSubmitted { get; set; }
    }
}
