using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.CostsScheduling;

public class CostsScheduleFireRiskCladdingWorksResult
{
    public DateTime? FraewSurveyDate { get; set; }

    public decimal? FraewSurveyCost { get; set; }

    public EReplacementCladding? FraewRemediationType { get; set; }

    public IList<CostsScheduleFireRiskCladdingSystemItemResult> CladdingWorks { get; set; } = new List<CostsScheduleFireRiskCladdingSystemItemResult>();
}
