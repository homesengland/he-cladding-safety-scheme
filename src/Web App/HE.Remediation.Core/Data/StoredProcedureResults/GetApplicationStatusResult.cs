using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetApplicationStatusResult
{
    public bool DeclarationConfirmed { get; set; }
    public bool Submitted { get; set; }
    public EApplicationStatus Status { get; set; }
    public EApplicationStage Stage { get; set; }
}