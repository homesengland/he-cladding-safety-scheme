using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.GetBuildingsInsurance;

namespace HE.Remediation.WebApp.ViewModels.BuildingsInsurance
{
    public class BuildingsInsuranceViewModel
    {
        public string SumInsuredAmountText { get; set; }

        public decimal? SumInsuredAmount =>
            decimal.TryParse(this.SumInsuredAmountText, out var amount)
                ? amount
                : null;
        public string CurrentBuildingInsurancePremiumAmountText { get; set; }

        public decimal? CurrentBuildingInsurancePremiumAmount =>
            decimal.TryParse(this.CurrentBuildingInsurancePremiumAmountText, out var amount)
                ? amount
                : null;
        public List<int> SelectedInsuranceProviderIds { get; set; } = [];
        public string IfOtherInsuranceProviderName { get; set; }
        public string AdditionalInfo { get; set; }
        public List<InsuranceProvider> InsuranceProviders { get; set; }
        public EBuildingRemediationResponsibilityType? BuildingRemediationResponsibilityTypeId { get; set; }
        public ESubmitAction SubmitAction { get; set; }
        public string ReturnUrl { get; set; }
        public string ReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public bool SubcontractorsRequired { get; set; }
        public bool IsSubmitted { get; set; }
    }
}
