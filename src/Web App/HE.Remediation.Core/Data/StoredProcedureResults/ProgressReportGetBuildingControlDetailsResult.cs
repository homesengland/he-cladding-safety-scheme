namespace HE.Remediation.Core.Data.StoredProcedureResults
{
    public class ProgressReportGetBuildingControlDetailsResult
    {
        public DateTime? ForecastDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public DateTime? ValidationDate { get; set; }
        public DateTime? DecisionDate { get; set; }
        public bool? Decision { get; set; }
    }
}
