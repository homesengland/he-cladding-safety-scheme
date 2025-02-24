using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.CostsScheduling;

public class CostsScheduleFireRiskCladdingSystemItemResult
{
    public Guid? FireRiskCladdingSystemsId { get; set; }

    public string CladdingSystemTypeName { get; set; }

    public ETaskStatus CladdingSystemTaskStatusId { get; set; }
}
