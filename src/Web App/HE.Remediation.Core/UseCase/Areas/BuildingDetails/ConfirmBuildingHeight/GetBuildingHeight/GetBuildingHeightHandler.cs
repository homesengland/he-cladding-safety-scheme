using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmBuildingHeight.GetBuildingHeight;

public class GetBuildingHeightHandler : IRequestHandler<GetBuildingHeightRequest, GetBuildingHeightResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetBuildingHeightHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetBuildingHeightResponse> Handle(GetBuildingHeightRequest request, CancellationToken cancellationToken)
    {
        var response = await _connection.QuerySingleOrDefaultAsync<GetBuildingHeightResponse>("GetBuildingHeight", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId()
        });

        return response ?? new GetBuildingHeightResponse();
    }
}