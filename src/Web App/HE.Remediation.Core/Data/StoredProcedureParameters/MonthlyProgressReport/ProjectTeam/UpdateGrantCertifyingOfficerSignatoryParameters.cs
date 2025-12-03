namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;

public class UpdateGrantCertifyingOfficerSignatoryParameters
{
    public Guid ProgressReportId { get; set; }
    public string Signatory { get; set; }
    public string EmailAddress { get; set; }
    public DateTime DateAppointed { get; set; }
}