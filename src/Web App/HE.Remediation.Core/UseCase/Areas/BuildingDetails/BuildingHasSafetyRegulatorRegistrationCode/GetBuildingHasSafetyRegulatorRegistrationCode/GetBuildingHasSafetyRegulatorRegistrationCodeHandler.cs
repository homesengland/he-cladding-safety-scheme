using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingHasSafetyRegulatorRegistrationCode.GetBuildingHasSafetyRegulatorRegistrationCode;

public class GetBuildingHasSafetyRegulatorRegistrationCodeHandler : IRequestHandler<GetBuildingHasSafetyRegulatorRegistrationCodeRequest, GetBuildingHasSafetyRegulatorRegistrationCodeResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetBuildingHasSafetyRegulatorRegistrationCodeHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetBuildingHasSafetyRegulatorRegistrationCodeResponse> Handle(GetBuildingHasSafetyRegulatorRegistrationCodeRequest request, CancellationToken cancellationToken)
    {
        var hasBuildingSafetyRegulatorRegistrationCode = await _connection.QuerySingleOrDefaultAsync<bool?>("GetBuildingHasSafetyRegulatorRegistrationCode", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId()
        });

        return new GetBuildingHasSafetyRegulatorRegistrationCodeResponse
        {
            BuildingHasSafetyRegulatorRegistrationCode = hasBuildingSafetyRegulatorRegistrationCode
        };
    }
}