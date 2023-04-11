using HE.Remediation.Core.Interface;
using MediatR;

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

    public async Task<GetAreaProgressResponse> Handle(GetAreaProgressRequest request, CancellationToken cancellationToken)
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
}