using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class SurveyInstructionDetailsViewModel
    {
        public int FireRiskAssessorId { get; set; }
        public DateTime? DateOfInstruction { get; set; }
        public List<FireRiskAssessorCompanyViewModel> FireRiskAssessorCompanies { get; set; }
        public ESubmitAction SubmitAction { get; set; }
    }
}
