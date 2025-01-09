using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingHasSafetyRegulatorRegistrationCode.SetBuildingHasSafetyRegulatorRegistrationCode;

public class SetBuildingHasSafetyRegulatorRegistrationCodeHandler : IRequestHandler<SetBuildingHasSafetyRegulatorRegistrationCodeRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetBuildingHasSafetyRegulatorRegistrationCodeHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<Unit> Handle(SetBuildingHasSafetyRegulatorRegistrationCodeRequest request, CancellationToken cancellationToken)
    {
        await SaveBuildingHasSafetyRegulatorRegistrationCode(request);
        return Unit.Value;
    }

    private async Task SaveBuildingHasSafetyRegulatorRegistrationCode(SetBuildingHasSafetyRegulatorRegistrationCodeRequest request)
    {
        await _connection.ExecuteAsync("UpdateBuildingHasSafetyRegulatorRegistrationCode", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            request.BuildingHasSafetyRegulatorRegistrationCode
        });
    }
}