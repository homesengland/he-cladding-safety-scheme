using HE.Remediation.Core.Interface;
using MediatR;

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

    public async Task<Unit> Handle(SetCladdingSystemRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        await _dbConnectionWrapper
            .ExecuteAsync("InsertOrUpdateCladdingSystems", new
            {
                ApplicationId = applicationId, 
                request.FireRiskCladdingSystemsId, 
                request.CladdingSystemTypeId, 
                request.InsulationTypeId
            });
        return Unit.Value;
    }
}