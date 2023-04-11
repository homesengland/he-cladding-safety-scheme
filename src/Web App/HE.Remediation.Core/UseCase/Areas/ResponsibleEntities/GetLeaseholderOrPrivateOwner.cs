using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

public class GetLeaseholderOrPrivateOwnerHandler : IRequestHandler<GetLeaseholderOrPrivateOwnerRequest, GetLeaseholderOrPrivateOwnerResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetLeaseholderOrPrivateOwnerHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetLeaseholderOrPrivateOwnerResponse> Handle(GetLeaseholderOrPrivateOwnerRequest request, CancellationToken cancellationToken)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetLeaseholderOrPrivateOwnerResponse>("GetHasOwners", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId()
        });

        return result ?? new GetLeaseholderOrPrivateOwnerResponse();
    }
}

public class GetLeaseholderOrPrivateOwnerResponse
{
    public bool? HasOwners { get; set; }
    public int? SharedOwnerCount { get; set; }
}

public class GetLeaseholderOrPrivateOwnerRequest : IRequest<GetLeaseholderOrPrivateOwnerResponse>
{
    private GetLeaseholderOrPrivateOwnerRequest()
    {
    }

    public static readonly GetLeaseholderOrPrivateOwnerRequest Request = new();
}