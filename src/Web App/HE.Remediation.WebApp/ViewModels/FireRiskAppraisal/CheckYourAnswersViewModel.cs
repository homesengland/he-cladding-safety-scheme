using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class CheckYourAnswersViewModel
    {
        // new fields below

        public string AppraisalReportFilename { get; set; }

        public string AppraisalSummaryFilename { get; set; }

        public string FireRiskAssessorCompany { get; set; }

        public DateTime? DateOfInstruction { get; set; }

        public DateTime? SurveyDate { get; set; }

        public string AuthorsName { get; set; }

        public string PeerReviewPerson { get; set; }

        public string UndertakingFirm { get; set; }

        public int? NumberOfStoreys { get; set; }    

        public int? BuildingHeight { get; set; }

        public ENoYes? BuildingInterimMeasures { get; set; }

        public EBasicComplexType? BasicComplexType { get; set; }

        public ENoYes? ExternalWorksRequired { get; set; }

        public ENoYes? InternalWorksRequired { get; set; }

        public string LifeSafetyRiskAssessment { get; set; }

        public string RecommendCladding { get; set; }

        public ENoYes? RecommendBuildingInterim { get; set; }

        public string CaveatsLimitations { get; set; }

        public string RemediationSummary { get; set; }

        public string JustifyRecommendation { get; set; }  

        // lists/collections
    
        public List<GetWallWorksListResult> InternalWorks { get; set; }

        public List<GetWallWorksListResult> ExternalWorks { get; set; }

        public List<CladdingSystemsListResult> CladdingSystems { get; set; }
           

        // old fields below
        public bool FireRiskCompleted { get; set; }
        public string AppraisalSurveyDetailsFireRiskAssessor { get; set; }
        public DateTime? AppraisalSurveyDetailsDateOfInstruction { get; set; }
        public DateTime? AppraisalSurveyDetailsSurveyDate { get; set; }
        public string AssessorDetailsFirstName { get; set; }
        public string AssessorDetailsLastName { get; set; }
        public string AssessorDetailsCompanyName { get; set; }
        public string AssessorDetailsCompanyNumber { get; set; }
        public string AssessorDetailsEmailAddress { get; set; }
        public string AssessorDetailsTelephone { get; set; }
        public string SurveyInstructionDetailsFireRiskAssessor { get; set; }
        public DateTime? SurveyInstructionDetailsDateOfInstruction { get; set; }
        public string FRAEWFileName { get; set; }
        public bool ReadOnly { get; set; }
    }
}
