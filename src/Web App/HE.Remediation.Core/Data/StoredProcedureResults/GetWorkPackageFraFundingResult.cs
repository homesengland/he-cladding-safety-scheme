using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetWorkPackageFraFundingResult
{
    public bool? HasFunding { get; set; }
    public EFraFundingType? FraFundingTypeId { get; set; }
}