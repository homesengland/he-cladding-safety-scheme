using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetFireRiskAssessmentForApplicationResult
{
    public EFireRiskAssessmentType? FireRiskAssessmentType { get; set; }
    public FileResult AddedFra { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}