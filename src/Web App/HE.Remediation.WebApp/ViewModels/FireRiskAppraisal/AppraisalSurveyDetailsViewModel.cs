using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class AppraisalSurveyDetailsViewModel
    { 
        public int? FireRiskAssessorId { get; set; }
        public DateTime? DateOfInstruction { get; set; }
        public DateTime? SurveyDate { get; set; }
        public List<FireRiskAssessorCompanyViewModel> FireRiskAssessorCompanies { get; set; }
        public bool FireAccessorNotOnPanel { get; set; }
        public ESubmitAction SubmitAction { get; set; }

        public string ReturnUrl { get; set; }
    }
}
