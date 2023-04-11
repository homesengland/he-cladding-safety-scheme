using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.SetAppraisalSurveyDetails
{
    public class SetAppraisalSurveyDetailsRequest : IRequest<Unit>
    {
        public int? FireRiskAssessorId { get; set; }
        public DateTime? DateOfInstruction { get; set; }
        public DateTime? SurveyDate { get; set; }
    }
}
