using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AddressDetails.Set;

public class SetAddressDetailsRequest : IRequest<Unit>
{
    public string SelectedAddressId { get; set; }
}
