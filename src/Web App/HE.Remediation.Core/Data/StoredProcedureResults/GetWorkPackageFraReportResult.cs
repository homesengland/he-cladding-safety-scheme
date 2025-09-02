using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetWorkPackageFraReportResult
{
    public EFireRiskAssessmentType FireRiskAssessmentTypeId { get; set; }
    public FileResult File { get; set; }
}