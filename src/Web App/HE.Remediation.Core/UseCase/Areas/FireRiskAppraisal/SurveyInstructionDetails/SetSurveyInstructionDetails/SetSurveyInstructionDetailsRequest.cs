using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.SurveyInstructionDetails.SetSurveyInstructionDetails
{
    public class SetSurveyInstructionDetailsRequest : IRequest<Unit>
    {
        public int FireRiskAssessorId { get; set; }
        public DateTime? DateOfInstruction { get; set; }
    }
}