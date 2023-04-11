using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.GetAppraisalSurveyDetails
{
    public class GetAppraisalSurveyDetailsResponse
    {
        public int? FireRiskAssessorId { get; set; }
        public DateTime? DateOfInstruction { get; set; }
        public List<GetFireRiskAssessorListResult> FireRiskAssessorCompanies { get; set; }
        public DateTime? SurveyDate { get; set; }
        public bool FireAccessorNotOnPanel { get; set; }
    }
}
