using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.GetCorrespondenceAddress;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.GetCorrespondenceAddress;

public class GetCorrespondenceAddressRequest : IRequest<GetCorrespondenceAddressResponse>
{
    public static readonly GetCorrespondenceAddressRequest Request = new();
}

