using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling
{
    public class CheckYourAnswersViewModel : WorkPackageBaseViewModel
    {
        public ENoYes CompetitiveBidsObtained { get; set; }
        public List<SubContractors> SubContractors { get; set; }
        public List<CladdingSystem> CladdingSystems { get; set; }
        public decimal EligibleCosts { get; set; }
        public decimal IneligibleCosts { get; set; }
        public decimal TotalCosts { get; set; }
        public string RemovalOfCladdingDescription { get; set; }
        public string NewCladdingDescription { get; set; }
        public string EligibleExternalDescription { get; set; }
        public string EligibleInternalDescription { get; set; }
        public string MainContractorDescription { get; set; }
        public string AccessDescription { get; set; }
        public string OverheadsProfitDescription { get; set; }
        public string ContingenciesDescription { get; set; }
        public string FeasibilityDescription { get; set; }
        public string PostTenderDescription { get; set; }
        public string PropertyManagerDescription { get; set; }
        public string VatDescription { get; set; }

    }

    public class CladdingSystem
    {
        public bool BeingRemoved { get; set; }
        public string CladdingType { get; set; }
        public string CladdingManufacturer { get; set; }
        public string InsulationMaterial { get; set; }
        public string InsulationManufacturer { get; set; }
    }

    public class SubContractors
    {
        public string CompanyRegistrationNumber { get; set; }
        public string CompanyName { get; set; }
    }
}
