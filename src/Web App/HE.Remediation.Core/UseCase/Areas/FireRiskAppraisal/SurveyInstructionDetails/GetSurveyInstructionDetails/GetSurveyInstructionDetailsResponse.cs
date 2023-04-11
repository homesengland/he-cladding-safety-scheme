using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.SurveyInstructionDetails.GetSurveyInstructionDetails
{
    public class GetSurveyInstructionDetailsResponse
    {
        public int FireRiskAssessorId { get; set; }
        public DateTime? DateOfInstruction { get; set; }
        public List<GetFireRiskAssessorListResult> FireRiskAssessorCompanies { get; set; }
    }
}
