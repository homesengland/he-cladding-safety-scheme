using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class ResidentialUnitsViewModel
    {
        public int? ResidentialUnitsCount { get; set; }
        public ENoYes? NonResidentialUnits { get; set; }
        public string ReturnUrl { get; set; }
    }
}
