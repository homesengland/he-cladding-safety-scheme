namespace HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.GrantCertifyingOfficer;

public class UpdateGrantCertifyingOfficerAuthorisedSignatoriesParameters
{
    public string AuthorisedSignatory1 { get; set; }

    public string AuthorisedSignatory1EmailAddress { get; set; }

    public DateTime? CompaniesDateOfAppointment { get; set; }
}
