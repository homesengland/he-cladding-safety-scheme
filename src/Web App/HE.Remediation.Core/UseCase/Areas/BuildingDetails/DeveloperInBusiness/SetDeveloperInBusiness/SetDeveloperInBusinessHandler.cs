using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperInBusiness.SetDeveloperInBusiness;

public class SetDeveloperInBusinessHandler : IRequestHandler<SetDeveloperInBusinessRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetDeveloperInBusinessHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<Unit> Handle(SetDeveloperInBusinessRequest request, CancellationToken cancellationToken)
    {
        await SaveDeverloperInBusiness(request);
        return Unit.Value;
    }

    private async Task SaveDeverloperInBusiness(SetDeveloperInBusinessRequest request)
    {
        await _connection.ExecuteAsync("UpdateDeveloperInBusiness", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            IsOriginalDeveloperStillInBusiness = (int)request.IsOriginalDeveloperStillInBusiness!.Value
        });
    }
}