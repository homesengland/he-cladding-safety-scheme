namespace HE.Remediation.WebApp.ViewModels.Document;

public class ApplicantDocumentsViewModel
{
    public string ApplicationReference { get; set; }
    public string BuildingName { get; set; }
    public string SearchTerm { get; set; }
    public IReadOnlyCollection<ApplicantDocumentViewModel> Files { get; set; }

    public class ApplicantDocumentViewModel
    {
        public Guid Id { get; set; }
        public string Filename { get; set; }
        public DateTime UploadDate { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
    }
}