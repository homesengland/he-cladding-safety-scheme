using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.GetCorrespondenceAddress;
using MediatR;
namespace HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.GetCorrespondenceAddress;

public class GetCorrespondenceAddressHandler : IRequestHandler<GetCorrespondenceAddressRequest, GetCorrespondenceAddressResponse>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IDbConnectionWrapper _db;

    public GetCorrespondenceAddressHandler(IApplicationDataProvider adp, IDbConnectionWrapper db)
    {
        _adp = adp;
        _db = db;
    }

    public async Task<GetCorrespondenceAddressResponse> Handle(GetCorrespondenceAddressRequest request, CancellationToken cancellationToken)
    {
        var userId = _adp.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot get fetch the correspondant address details due to an unknown User Id.");
        }

        var result = await _db.QuerySingleOrDefaultAsync<GetCorrespondenceAddressResponse>(
            "GetCorrespondanceAddressDetails", new 
            { 
                userId 
            });

        return result ?? new GetCorrespondenceAddressResponse();
    }
}

