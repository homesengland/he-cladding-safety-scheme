using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AuthorisedSignatories.Set;

public class SetAuthorisedSignatoriesRequest : IRequest
{
    public string AuthorisedSignatory1 { get; set; }

    public string AuthorisedSignatory1EmailAddress { get; set; }

    public int? CompaniesDateOfAppointmentDay { get; set; }

    public int? CompaniesDateOfAppointmentMonth { get; set; }

    public int? CompaniesDateOfAppointmentYear { get; set; }
}
