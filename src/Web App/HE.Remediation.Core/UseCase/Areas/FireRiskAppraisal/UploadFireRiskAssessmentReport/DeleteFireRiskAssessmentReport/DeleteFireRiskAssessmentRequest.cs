using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAssessmentReport.DeleteFireRiskAssessmentReport
{
    public class DeleteFireRiskAssessmentRequest : IRequest
    {
        public Guid FileId { get; set; }
    }
}
