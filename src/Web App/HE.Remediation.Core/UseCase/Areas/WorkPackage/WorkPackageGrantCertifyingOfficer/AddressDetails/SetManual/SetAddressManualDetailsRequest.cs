using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AddressDetails.SetManual;

public class SetAddressManualDetailsRequest : IRequest<Unit>
{
    public string NameNumber { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string City { get; set; }

    public string County { get; set; }

    public string Postcode { get; set; }
}
