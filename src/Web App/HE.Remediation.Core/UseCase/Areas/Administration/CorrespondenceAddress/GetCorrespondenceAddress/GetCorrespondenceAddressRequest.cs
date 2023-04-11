using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.GetCorrespondenceAddress;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.GetCorrespondenceAddress;

public class GetCorrespondenceAddressRequest : IRequest<GetCorrespondenceAddressResponse>
{
    public static readonly GetCorrespondenceAddressRequest Request = new();
}

