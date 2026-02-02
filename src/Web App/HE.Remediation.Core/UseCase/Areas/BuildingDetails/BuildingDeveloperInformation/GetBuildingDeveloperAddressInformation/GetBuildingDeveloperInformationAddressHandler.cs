
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.GetBuildingDeveloperAddressInformation;

public class GetBuildingDeveloperInformationAddressHandler: IRequestHandler<GetBuildingDeveloperInformationAddressRequest, GetBuildingDeveloperInformationAddressResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetBuildingDeveloperInformationAddressHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetBuildingDeveloperInformationAddressResponse> Handle(GetBuildingDeveloperInformationAddressRequest request, CancellationToken cancellationToken)
    {
        var response = await _connection.QuerySingleOrDefaultAsync<GetBuildingDeveloperInformationAddressResponse>("GetBuildingDeveloperInformation", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId()
        });

        return response ?? new GetBuildingDeveloperInformationAddressResponse();
    }
}
