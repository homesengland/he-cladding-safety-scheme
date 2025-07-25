﻿using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CheckYourAnswers.GetCheckYourAnswers;

public class GetCheckYourAnswersResponse
{
    public string AppraisalReportFilename { get; set; }

    public string AppraisalSummaryFilename { get; set; }

    public string AssessmentFilename { get; set; }
    public EFireRiskAssessmentType? FireRiskAssessmentType { get; set; }

    public string FireRiskAssessorCompany { get; set; }

    public string AssessorDetailsFirstName { get; set; }
    public string AssessorDetailsLastName { get; set; }
    public string AssessorDetailsCompanyName { get; set; }
    public string AssessorDetailsCompanyNumber { get; set; }
    public string AssessorDetailsEmailAddress { get; set; }
    public string AssessorDetailsTelephone { get; set; }

    public DateTime? DateOfInstruction { get; set; }

    public DateTime? SurveyDate { get; set; }

    public string AuthorsName { get; set; }

    public string PeerReviewPerson { get; set; }

    public decimal? FraewCost { get; set; }

    public int? NumberOfStoreys { get; set; }

    public decimal? BuildingHeight { get; set; }

    public EBasicComplexType? BasicComplexType { get; set; }

    public string CladdingSystemType { get; set; }

    public string InsulationType { get; set; }


    public ENoYes? ExternalWorksRequired { get; set; }


    public ENoYes? InternalWorksRequired { get; set; }

    public string LifeSafetyRiskAssessment { get; set; }

    public string RecommendCladding { get; set; }

    public ENoYes? RecommendBuildingInterim { get; set; }

    public int? RecommendTotalAreaCladding { get; set; }

    public string CaveatsLimitations { get; set; }

    public string RemediationSummary { get; set; }

    public string JustifyRecommendation { get; set; }

    public string InterimMeasuresOtherText { get; set; }

    public string SafetyRiskOtherText { get; set; }

    public string OtherRiskMitigationOptionsConsidered { get; set; }

    // lists/collections

    public List<GetWallWorksListResult> InternalWorks { get; set; }

    public List<GetWallWorksListResult> ExternalWorks { get; set; }

    public List<CladdingSystemsListResult> CladdingSystems { get; set; }

    public List<ERiskSafetyMitigationType> SafetyRiskMitigationOptions { get; set; }

    public List<EInterimMeasuresType> InterimMeasureOptions { get; set; }

    public bool ReadOnly { get; set; }

    //interim measures
    public EYesNoNonBoolean? BuildingInterimMeasures { get; set; }

    public EEvacuationStrategy? EvacuationStrategyType { get; set; }

    public int? NumberOfStairwells { get; set; }

    public EYesNoNonBoolean? ExternalWallAndBalconiesPolicy { get; set; }

    public EYesNoNonBoolean? FireAndResueAccessRestrictions { get; set; }

    public string FireAndResueAccessRestrictionsText { get; set; }

    public IEnumerable<EInterimMeasuresType> BuildingInterimMeasuresTypes { get; set; }
}