using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Application.Dashboard.SchemeSelection;

using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.GetBuildingDeveloperInformation;

public class GetBuildingDeveloperInformationHandler : IRequestHandler<GetBuildingDeveloperInformationRequest, GetBuildingDeveloperInformationResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetBuildingDeveloperInformationHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetBuildingDeveloperInformationResponse> Handle(GetBuildingDeveloperInformationRequest request, CancellationToken cancellationToken)
    {
        var response = await _connection.QuerySingleOrDefaultAsync<GetBuildingDeveloperInformationResponse>("GetBuildingOriginalDeveloperIsKnown", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
        });

        response ??= new GetBuildingDeveloperInformationResponse();

        response.ApplicationScheme = _applicationDataProvider.GetApplicationScheme();

        return response;

    }
}