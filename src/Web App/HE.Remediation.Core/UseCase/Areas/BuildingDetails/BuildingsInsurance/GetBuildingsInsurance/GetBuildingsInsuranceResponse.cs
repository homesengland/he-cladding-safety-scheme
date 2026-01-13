using HE.Remediation.Core.Enums;
using System.Collections.Generic;
using System.Text.Json;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.GetBuildingsInsurance;

public class GetBuildingsInsuranceResponse
{
    private List<int> _selectedInsuranceProviderIds { get; set; } = [];
    public decimal? SumInsuredAmount { get; set; }
    public decimal? CurrentBuildingInsurancePremiumAmount { get; set; }
    public string IfOtherInsuranceProviderName { get; set; }
    public string AdditionalInfo { get; set; }
    public EBuildingRemediationResponsibilityType? BuildingRemediationResponsibilityTypeId { get; set; }
    public List<InsuranceProvider> InsuranceProviders { get; set; }
    public List<int> SelectedInsuranceProviderIds
    { 
        get { 
            if (!string.IsNullOrEmpty(InsuranceProvidersJson))
            {
                var selectedInsuranceProviders = JsonSerializer.Deserialize<List<InsuranceProvider>>(InsuranceProvidersJson);
                _selectedInsuranceProviderIds = selectedInsuranceProviders.Select(i => i.Id).ToList();
            }
            return _selectedInsuranceProviderIds;
        }
    }
    public string InsuranceProvidersJson { get; set; }
    public string ReferenceNumber {  get; set; }
    public string BuildingName {  get; set; }

}