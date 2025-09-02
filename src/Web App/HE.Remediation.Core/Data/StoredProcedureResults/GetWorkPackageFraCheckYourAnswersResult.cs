using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetWorkPackageFraCheckYourAnswersResult
{
    public string FraFile { get; set; }
    public EFireRiskAssessmentType? FireRiskAssessmentTypeId { get; set; }
    public string FireRiskAssessor { get; set; }
    public string OtherAssessorFirstName { get; set; }
    public string OtherAssessorLastName { get; set; }
    public string OtherAssessorCompanyName { get; set; }
    public string OtherAssessorCompanyNumber { get; set; }
    public string OtherAssessorEmailAddress { get; set; }
    public string OtherAssessorTelephone { get; set; }
    public DateTime? FireRiskAssessmentDate { get; set; }
    public EFraRiskRating? FireRiskRatingId { get; set; }
    public bool HasInternalFireSafetyRisks { get; set; }
    public bool HasFunding { get; set; }
    public EFraFundingType? FraFundingTypeId { get; set; }
    public string OtherInternalFireSafetyRisk { get; set; }
    public bool IsSubmitted { get; set; }

    public IList<string> Defects { get; set; } = new List<string>();

    public class Defect
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}