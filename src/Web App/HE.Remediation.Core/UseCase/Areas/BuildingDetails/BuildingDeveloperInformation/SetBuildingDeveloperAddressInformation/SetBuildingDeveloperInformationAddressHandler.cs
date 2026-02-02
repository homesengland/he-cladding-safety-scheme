using Dapper;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.SetBuildingDeveloperAddressInformation;

public class SetBuildingDeveloperInformationAddressHandler : IRequestHandler<SetBuildingDeveloperInformationAddressRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetBuildingDeveloperInformationAddressHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<Unit> Handle(SetBuildingDeveloperInformationAddressRequest request, CancellationToken cancellationToken)
    {
        await SaveBuildingDeveloperInformation(request);
        return Unit.Value;
    }

    private async Task SaveBuildingDeveloperInformation(SetBuildingDeveloperInformationAddressRequest request)
    {
        var parameters = new DynamicParameters(request);
        parameters.Add("@ApplicationId", _applicationDataProvider.GetApplicationId());

        await _connection.ExecuteAsync("UpdateBuildingDeveloperInformation", parameters);
    }
}
