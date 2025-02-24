using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AddressDetails.Get;

public class GetAddressDetailsRequest : IRequest<GetAddressDetailsResponse>
{
    private GetAddressDetailsRequest()
    {
    }

    public static readonly GetAddressDetailsRequest Request = new();
}