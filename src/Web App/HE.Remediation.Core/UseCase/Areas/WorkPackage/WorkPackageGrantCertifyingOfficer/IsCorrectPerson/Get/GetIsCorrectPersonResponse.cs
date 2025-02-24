using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.IsCorrectPerson.Get;

public class GetIsCorrectPersonResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public ECertifyingOfficerResponse CertifyingOfficerResponse { get; set; }

    public bool IsSubmitted { get; set; }
}
