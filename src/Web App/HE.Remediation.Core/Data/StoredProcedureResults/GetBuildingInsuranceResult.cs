using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetBuildingInsuranceResult
{
    public decimal? SumInsuredAmount { get; set; }
    public decimal? CurrentBuildingInsurancePremiumAmount { get; set; }
    public string IfOtherInsuranceProviderName { get; set; }
    public string AdditionalInfo { get; set; }
    public EBuildingRemediationResponsibilityType? BuildingRemediationResponsibilityTypeId { get; set; }
    public string InsuranceProvidersJson { get; set; }
}