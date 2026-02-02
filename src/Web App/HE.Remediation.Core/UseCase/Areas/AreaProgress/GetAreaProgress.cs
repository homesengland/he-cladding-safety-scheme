using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.AreaProgress;

public class GetAreaProgressHandler : IRequestHandler<GetAreaProgressRequest, GetAreaProgressResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetAreaProgressHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetAreaProgressResponse> Handle(GetAreaProgressRequest request, CancellationToken cancellationToken)
    {
        var response = await _connection.QuerySingleOrDefaultAsync<GetAreaProgressResponse>("GetAreaProgress", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            request.Area
        });

        return response;
    }
}

public class GetAreaProgressRequest : IRequest<GetAreaProgressResponse>
{
    public string Area { get; set; }
}

public class GetAreaProgressResponse
{
    public string Area { get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }
    public string RouteDataJson { get; set; }
    public string AreaDataJson { get; set; }
}