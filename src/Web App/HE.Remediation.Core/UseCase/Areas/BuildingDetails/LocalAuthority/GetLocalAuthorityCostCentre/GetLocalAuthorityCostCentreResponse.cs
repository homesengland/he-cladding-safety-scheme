namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.LocalAuthority.GetLocalAuthorityCostCentre
{
    public class GetLocalAuthorityCostCentreResponse
    {
        public string LocalAuthorityCostCentreId {  get; set; }
        public List<LocalAuthorityCostCentre> LocalAuthorityCostCentres { get; set; }
    }
}
