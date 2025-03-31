namespace HE.Remediation.Core.Settings
{
    public class FileServiceSettings
    {
        public UploadSectionSettings LeaseHolderEvidence { get; set; }
        public UploadSectionSettings FireRiskAppraisal { get; set; }
        public UploadSectionSettings ResponsibleEntitiesEvidence { get; set; }
        public UploadSectionSettings FireRiskAppraisalSummary { get; set; }
        public UploadSectionSettings ProgressReportEvidence { get; set; }
        public UploadSectionSettings ScheduleOfWorksContract { get; set; }
        public UploadSectionSettings ScheduleOfWorksBuildingControl { get; set; }
        public UploadSectionSettings ScheduleOfWorksLeaseholderEngagement { get; set; }
        public UploadSectionSettings PaymentRequestEvidence { get; set; }
        public UploadSectionSettings VariationRequestEvidence { get; set; }
        public UploadSectionSettings ClosingReport { get; set; }
        public UploadSectionSettings PaymentRequestInvoice { get; set; }
        public UploadSectionSettings ProjectPlan { get; set; }
    }

    public class UploadSectionSettings
    {
        public string[] AcceptedFileTypes { get; set; }
        public int MaximumFileSizeMb { get; set; }
    }
}
