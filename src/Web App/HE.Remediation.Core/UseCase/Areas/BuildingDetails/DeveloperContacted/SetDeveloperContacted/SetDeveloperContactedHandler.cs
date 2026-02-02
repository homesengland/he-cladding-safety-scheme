using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperContacted.SetDeveloperContacted;

public class SetDeveloperContactedHandler : IRequestHandler<SetDeveloperContactedRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetDeveloperContactedHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<Unit> Handle(SetDeveloperContactedRequest request, CancellationToken cancellationToken)
    {
        await SaveDeveloperContacted(request);
        return Unit.Value;
    }

    private async Task SaveDeveloperContacted(SetDeveloperContactedRequest request)
    {
        await _connection.ExecuteAsync("UpdateDeveloperContacted", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            request.HasDeveloperBeenContactedAboutRemediation
        });
    }
}