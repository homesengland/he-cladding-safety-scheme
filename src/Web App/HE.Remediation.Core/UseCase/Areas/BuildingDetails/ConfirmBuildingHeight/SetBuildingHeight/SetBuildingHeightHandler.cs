using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmBuildingHeight.SetBuildingHeight;

public class SetBuildingHeightHandler : IRequestHandler<SetBuildingHeightRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetBuildingHeightHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<Unit> Handle(SetBuildingHeightRequest request, CancellationToken cancellationToken)
    {
        await SaveBuildingHeight(request);
        return Unit.Value;
    }

    private async Task SaveBuildingHeight(SetBuildingHeightRequest request)
    {
        await _connection.ExecuteAsync("UpdateBuildingHeight", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            request.NumberOfStoreys
        });
    }
}