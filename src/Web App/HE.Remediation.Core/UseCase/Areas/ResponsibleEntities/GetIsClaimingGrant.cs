using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

public class GetIsClaimingGrantHandler : IRequestHandler<GetIsClaimingGrantRequest, GetIsClaimingGrantResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetIsClaimingGrantHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetIsClaimingGrantResponse> Handle(GetIsClaimingGrantRequest request, CancellationToken cancellationToken)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetIsClaimingGrantResponse>("GetIsClaimingGrant", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId()
        });

        return result ?? new GetIsClaimingGrantResponse();
    }
}

public class GetIsClaimingGrantResponse
{
    public bool? IsClaimingGrant { get; set; }
    public bool? HasOwners { get; set; }
}

public class GetIsClaimingGrantRequest : IRequest<GetIsClaimingGrantResponse>
{
    private GetIsClaimingGrantRequest()
    {
    }

    public static readonly GetIsClaimingGrantRequest Request = new();
}