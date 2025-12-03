using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.SetAppraisalSurveyDetails
{
    public class SetAppraisalSurveyDetailsRequest : IRequest<Unit>
    {
        public int? FireRiskAssessorId { get; set; }
        public DateTime? DateOfInstruction { get; set; }
        public DateTime? SurveyDate { get; set; }
        public bool? CommissionedByDeveloper { get; set; }
        public DateTime? ReceivedByDeveloperDate { get; set; }
        public bool? ReceivedByResponsibleEntity { get; set; }
    }
}
