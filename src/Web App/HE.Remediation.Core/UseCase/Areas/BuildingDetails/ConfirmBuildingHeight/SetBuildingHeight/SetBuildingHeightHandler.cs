using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.BuildingDetails.ConfirmBuildingHeight;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmBuildingHeight.SetBuildingHeight;

public class SetBuildingHeightHandler : IRequestHandler<SetBuildingHeightRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;

    public SetBuildingHeightHandler(IApplicationDataProvider applicationDataProvider, IBuildingDetailsRepository buildingDetailsRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
    }

    public async ValueTask<Unit> Handle(SetBuildingHeightRequest request, CancellationToken cancellationToken)
    {
        await SaveBuildingHeight(request);
        return Unit.Value;
    }

    private async Task SaveBuildingHeight(SetBuildingHeightRequest request)
    {
        var parameters = new SetBuildingHeightParameters
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            NumberOfStoreys = request.NumberOfStoreys,
            BuildingHeight = request.BuildingHeight
        };
        await _buildingDetailsRepository.UpdateBuildingHeight(parameters);
    }
}