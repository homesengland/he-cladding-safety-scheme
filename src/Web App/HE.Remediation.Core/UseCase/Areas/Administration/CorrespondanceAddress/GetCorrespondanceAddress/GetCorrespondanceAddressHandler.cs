using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CorrespondanceAddress.GetCorrespondanceAddress;

public class GetCorrespondanceAddressHandler : IRequestHandler<GetCorrespondanceAddressRequest, GetCorrespondanceAddressResponse>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IDbConnectionWrapper _db;

    public GetCorrespondanceAddressHandler(IApplicationDataProvider adp, IDbConnectionWrapper db)
    {
        _adp = adp;
        _db = db;
    }

    public async Task<GetCorrespondanceAddressResponse> Handle(GetCorrespondanceAddressRequest request, CancellationToken cancellationToken)
    {
        var userId = _adp.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot get fetch the correspondant address details due to an unknown User Id.");
        }

        var result = await _db.QuerySingleOrDefaultAsync<GetCorrespondanceAddressResponse>(
            "GetCorrespondanceAddressDetails", new 
            { 
                userId 
            });

        return result ?? new GetCorrespondanceAddressResponse();
    }
}

