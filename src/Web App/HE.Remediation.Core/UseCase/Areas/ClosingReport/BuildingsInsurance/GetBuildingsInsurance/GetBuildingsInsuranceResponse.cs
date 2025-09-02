using System.Collections.Generic;
using System.Text.Json;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.GetBuildingsInsurance;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.BuildingsInsurance.GetBuildingsInsurance;

public class GetBuildingsInsuranceResponse
{
    private List<int> _selectedInsuranceProviderIds { get; set; } = [];
    public decimal? SumInsuredAmount { get; set; }
    public decimal? CurrentBuildingInsurancePremiumAmount { get; set; }
    public int InsuranceProviderId { get; set; }
    public string IfOtherInsuranceProviderName { get; set; }
    public string AdditionalInfo { get; set; }
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

    public string SelectedInsuranceProviderCommaSeparatedList
    {
        get
        {
            var selectedInsurerText = string.Empty;
            if (!string.IsNullOrEmpty(InsuranceProvidersJson))
            {
                var otherText = !string.IsNullOrEmpty(IfOtherInsuranceProviderName) ? $", Other - {IfOtherInsuranceProviderName}" : string.Empty;
                var selectedInsuranceProviders = JsonSerializer.Deserialize<List<InsuranceProvider>>(InsuranceProvidersJson);
                selectedInsurerText = $"{string.Join(", ",selectedInsuranceProviders.Select(x => x.Name))} { otherText}";
            }
            return selectedInsurerText;
        }
    }
    public string InsuranceProvidersJson { get; set; }
    public string ReferenceNumber {  get; set; }
    public string BuildingName {  get; set; }
    public bool IsSubmitted { get; set; }

}