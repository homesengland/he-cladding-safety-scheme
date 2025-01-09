using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;

public class InsertCladdingSystemParameters
{
    public Guid FireRiskCladdingSystemsId { get; set; }

    public EReplacementCladding? IsBeingRemoved { get; set; }
}