
namespace HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.GetFinalCheckYourAnswers
{
    public class GetFinalCheckYourAnswersResponse
    {
        public string ReasonForClosing { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public bool IsSubmitted { get; set; }
    }
}
