namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class UpdateGrantCertifyingOfficerSignatoryParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public string Signatory { get; set; }
    public string EmailAddress { get; set; }
    public DateTime DateAppointed { get; set; }
}