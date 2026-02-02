using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.DeleteFireRiskAppraisalReport
{
    public class DeleteFireRiskAppraisalRequest: IRequest
    {
        public Guid FileId { get; set; }
    }
}
