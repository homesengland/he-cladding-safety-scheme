namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class BuildingControlDetailsViewModel
    {
        public int? ForecastDateMonth { get; set; }
        public int? ForecastDateYear { get; set; }
        public int? ActualDateMonth { get; set; }
        public int? ActualDateYear { get; set; }
        public int? ValidationDateMonth { get; set; }
        public int? ValidationDateYear { get; set; }
        public int? DecisionDateMonth { get; set; }
        public int? DecisionDateYear { get; set; }
        public bool? Decision { get; set; }
        public string BuildingName { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public string ReturnUrl { get; set; }
        public int Version { get; set; }
    }
}
