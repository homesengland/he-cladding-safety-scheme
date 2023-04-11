using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CorrespondanceAddress.GetCorrespondanceAddress;

public class GetCorrespondanceAddressRequest : IRequest<GetCorrespondanceAddressResponse>
{
    public static readonly GetCorrespondanceAddressRequest Request = new();
}

