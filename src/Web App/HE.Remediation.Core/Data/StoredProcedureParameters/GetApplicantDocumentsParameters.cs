namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class GetApplicantDocumentsParameters
{
    public Guid ApplicationId { get; set; }
    public string SearchTerm { get; set; }
}