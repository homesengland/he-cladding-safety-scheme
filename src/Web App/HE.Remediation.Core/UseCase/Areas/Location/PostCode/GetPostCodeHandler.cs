using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Location;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Location.PostCode;

public class GetPostCodeHandler : IRequestHandler<GetPostCodeRequest, GetPostCodeResponse>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IDbConnectionWrapper _db;
    private readonly IPostCodeLookup _pcl;

    public GetPostCodeHandler(IApplicationDataProvider adp, IDbConnectionWrapper db, IPostCodeLookup pcl)
    {
        _adp = adp;
        _db = db;
        _pcl = pcl;
    }

    public async Task<GetPostCodeResponse> Handle(GetPostCodeRequest request, CancellationToken cancellationToken)
    {
        var userId = _adp.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException("Could not fetch the User's id from within GetPostCodeHandler");
        }

        var results = await _pcl.SearchPostCode(request.PostCode);
        return new GetPostCodeResponse(results);
    }
}
