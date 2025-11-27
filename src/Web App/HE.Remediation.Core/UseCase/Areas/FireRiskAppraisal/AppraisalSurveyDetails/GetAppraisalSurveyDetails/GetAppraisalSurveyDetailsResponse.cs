using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.GetAppraisalSurveyDetails
{
    public class GetAppraisalSurveyDetailsResponse
    {
        public EApplicationScheme ApplicationScheme { get; set; }
        public int? FireRiskAssessorId { get; set; }
        public DateTime? DateOfInstruction { get; set; }
        public DateTime? SurveyDate { get; set; }
        public bool? CommissionedByDeveloper { get; set; }
        public DateTime? ReceivedByDeveloperDate { get; set; }
        public bool? ReceivedByResponsibleEntity { get; set; }
        public List<GetFireRiskAssessorListResult> FireRiskAssessorCompanies { get; set; }
        public bool FireAccessorNotOnPanel { get; set; }
    }
}
