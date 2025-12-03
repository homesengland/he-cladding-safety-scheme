namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class FileResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }
    public int Size { get; set; }
    public DateTime? UploadDate { get; set; }
}