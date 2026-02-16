using Dapper;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.SetBuildingDeveloperInformation;

public class SetBuildingDeveloperInformationHandler : IRequestHandler<SetBuildingDeveloperInformationRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetBuildingDeveloperInformationHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<Unit> Handle(SetBuildingDeveloperInformationRequest request, CancellationToken cancellationToken)
    {
        await SaveBuildingDeveloperInformation(request);
        return Unit.Value;
    }

    private async Task SaveBuildingDeveloperInformation(SetBuildingDeveloperInformationRequest request)
    {
        var parameters = new DynamicParameters(request);
        parameters.Add("@ApplicationId", _applicationDataProvider.GetApplicationId());

        await _connection.ExecuteAsync("SetBuildingOriginalDeveloperIsKnown", parameters);
    }
}