namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetApplicantDocumentsResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime UploadDate { get; set; }
    public string Category { get; set; }
    public string Type { get; set; }
}