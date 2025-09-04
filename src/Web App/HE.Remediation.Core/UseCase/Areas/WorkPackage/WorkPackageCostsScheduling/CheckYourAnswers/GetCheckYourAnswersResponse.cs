using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.CostsScheduling;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CheckYourAnswers
{
    public class GetCheckYourAnswersResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public bool IsSubmitted { get; set; }
        public ENoYes? CompetitiveBidsObtained { get; set; }
        public List<SubContractor> SubContractors { get; set; }
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

        public PreferredContractorLinksResult PreferredContractorLinksResult { get; set; }

        //public class CladdingSystem
        //{
        //    public bool BeingRemoved { get; set; }
        //    public string CladdingType { get; set; }
        //    public string CladdingManufacturer { get; set; }
        //    public string InsulationMaterial { get; set; }
        //    public string InsulationManufacturer { get; set; }
        //}

        public class SubContractor
        {
            public string CompanyRegistrationNumber { get; set; }
            public string CompanyName { get; set; }
        }
    }
}
