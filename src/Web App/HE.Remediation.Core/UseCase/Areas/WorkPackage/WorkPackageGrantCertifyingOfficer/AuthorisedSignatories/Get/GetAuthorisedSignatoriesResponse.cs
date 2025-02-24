namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AuthorisedSignatories.Get;

public class GetAuthorisedSignatoriesResponse
{
    public string BuildingName { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string AuthorisedSignatory1 { get; set; }

    public string AuthorisedSignatory1EmailAddress { get; set; }

    public int? CompaniesDateOfAppointmentDay { get; set; }

    public int? CompaniesDateOfAppointmentMonth { get; set; }

    public int? CompaniesDateOfAppointmentYear { get; set; }

    public bool IsSubmitted { get; set; }
}
