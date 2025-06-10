using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.GetBuildingsInsurance;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.BuildingsInsurance.SetBuildingsInsurance;

public class SetBuildingsInsuranceResponse
{
    public Guid Id { get; set; }
    public decimal? SumInsuredAmount { get; set; }
    public decimal? CurrentBuildingInsurancePremiumAmount { get; set; }
    public int InsuranceProviderId { get; set; }
    public string IfOtherInsuranceProviderName { get; set; }
    public string AdditionalInfo { get; set; }
    public List<InsuranceProvider> InsuranceProviders { get; internal set; }
}