using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.SetCladdingSystem;

public class SetCladdingSystemHandler : IRequestHandler<SetCladdingSystemRequest, Unit>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetCladdingSystemHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
    {
        _dbConnectionWrapper = dbConnectionWrapper;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<Unit> Handle(SetCladdingSystemRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        if (request.CladdingManufacturerId != 27)
        {
            request.OtherCladdingManufacturer = null;
        }

        if (request.InsulationManufacturerId != 27)
        {
            request.OtherInsulationManufacturer = null;
        }

        if (request.CladdingSystemTypeId != (int)ECladdingSystemType.Other)
        {
            request.OtherCladdingType = null;
        }

        if (request.InsulationTypeId != (int)EInsulationType.Other)
        {
            request.OtherInsulationType = null;
        }

        await _dbConnectionWrapper
            .ExecuteAsync("InsertOrUpdateCladdingSystems", new
            {
                ApplicationId = applicationId,
                request.FireRiskCladdingSystemsId,
                request.CladdingSystemTypeId,
                request.InsulationTypeId,
                request.CladdingManufacturerId,
                request.InsulationManufacturerId,
                request.OtherInsulationManufacturer,
                request.OtherCladdingManufacturer,
                request.OtherCladdingType,
                request.OtherInsulationType
            });
        return Unit.Value;
    }
}