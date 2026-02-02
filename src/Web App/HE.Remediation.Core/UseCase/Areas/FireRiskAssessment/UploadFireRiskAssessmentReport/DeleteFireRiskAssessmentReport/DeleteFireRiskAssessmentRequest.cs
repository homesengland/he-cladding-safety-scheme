using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment.UploadFireRiskAssessmentReport.DeleteFireRiskAssessmentReport
{
    public class DeleteFireRiskAssessmentRequest : IRequest
    {
        public Guid FileId { get; set; }
    }
}
