namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenStartOnSite.GetWhenStartOnSite
{
    public class GetWhenStartOnSiteResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public int? StartMonth { get; set; }
        public int? StartYear { get; set; }
        public int Version { get; set; }
    }
}
