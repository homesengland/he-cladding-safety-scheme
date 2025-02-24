using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class LocalAuthorityCostCentreViewModel
    {
        public ESubmitAction SubmitAction { get; set; }
        public string ReturnUrl { get; set; }
        public string LocalAuthorityCostCentreId {  get; set; }
        public List<LocalAuthorityCostCentre> LocalAuthorityCostCentres { get; set; }
    }

    public class LocalAuthorityCostCentre
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
