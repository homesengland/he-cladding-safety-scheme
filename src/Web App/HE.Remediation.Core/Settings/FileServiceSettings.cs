namespace HE.Remediation.Core.Settings
{
    public class FileServiceSettings
    {
        public UploadSectionSettings LeaseHolderEvidence { get; set; }
        public UploadSectionSettings FireRiskAppraisal { get; set; }
        public UploadSectionSettings ResponsibleEntitiesEvidence { get; set; }
        public UploadSectionSettings FireRiskAppraisalSummary { get; set; }
    }

    public class UploadSectionSettings
    {
        public string[] AcceptedFileTypes { get; set; }
        public int MaximumFileSizeMb { get; set; }
    }
}
