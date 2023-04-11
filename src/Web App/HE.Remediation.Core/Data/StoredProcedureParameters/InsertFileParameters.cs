namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class InsertFileParameters
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }
    public string MimeType { get; set; }
    public long Size { get; set; }
}