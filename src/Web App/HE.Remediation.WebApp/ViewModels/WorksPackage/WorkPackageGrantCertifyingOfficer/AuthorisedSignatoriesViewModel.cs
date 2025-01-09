using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageGrantCertifyingOfficer;

public class AuthorisedSignatoriesViewModel : WorkPackageBaseViewModel
{
    public string AuthorisedSignatory1 { get; set; }

    public string AuthorisedSignatory1EmailAddress { get; set; }

    public int? CompaniesDateOfAppointmentDay { get; set; }

    public int? CompaniesDateOfAppointmentMonth { get; set; }

    public int? CompaniesDateOfAppointmentYear { get; set; }
}
