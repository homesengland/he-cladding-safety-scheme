﻿using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.AddExtraContact.GetExtraContact;

public class GetExtraContactHandler: IRequestHandler<GetExtraContactRequest, GetExtraContactResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IDbConnectionWrapper _db;

    public GetExtraContactHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
    {
        _applicationDataProvider = applicationDataProvider;
        _db = db;
    }

    public async Task<GetExtraContactResponse> Handle(GetExtraContactRequest request, CancellationToken cancellationToken)
    {
        var userId = _applicationDataProvider.GetUserId();
        return await GetUserExtraContactResponse(request);
    }

    private async Task<GetExtraContactResponse> GetUserExtraContactResponse(GetExtraContactRequest request)
    {
        var userId = _applicationDataProvider.GetUserId();

        var result = await _db.QuerySingleOrDefaultAsync<GetExtraContactResponse>("GetUserAddSecondaryContactState", new { userId });
        return result ?? new GetExtraContactResponse();                        
    }
}
