using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperContacted.GetDeveloperContacted;

public class GetDeveloperContactedHandler : IRequestHandler<GetDeveloperContactedRequest, GetDeveloperContactedResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetDeveloperContactedHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetDeveloperContactedResponse> Handle(GetDeveloperContactedRequest request, CancellationToken cancellationToken)
    {
        var isDeveloperContacted = await _connection.QuerySingleOrDefaultAsync<bool?>("GetDeveloperContacted", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId()
        });

        return new GetDeveloperContactedResponse
        {
            HasDeveloperBeenContactedAboutRemediation = isDeveloperContacted
        };
    }
}