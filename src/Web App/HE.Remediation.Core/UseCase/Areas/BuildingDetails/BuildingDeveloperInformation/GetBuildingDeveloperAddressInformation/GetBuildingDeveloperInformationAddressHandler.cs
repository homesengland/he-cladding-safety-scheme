
using HE.Remediation.Core.Interface;
using MediatR;

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

    public async Task<GetBuildingDeveloperInformationAddressResponse> Handle(GetBuildingDeveloperInformationAddressRequest request, CancellationToken cancellationToken)
    {
        var response = await _connection.QuerySingleOrDefaultAsync<GetBuildingDeveloperInformationAddressResponse>("GetBuildingDeveloperInformation", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId()
        });

        return response ?? new GetBuildingDeveloperInformationAddressResponse();
    }
}
