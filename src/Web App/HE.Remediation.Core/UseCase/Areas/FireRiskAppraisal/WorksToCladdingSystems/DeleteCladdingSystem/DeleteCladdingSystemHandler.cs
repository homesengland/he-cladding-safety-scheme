using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.DeleteCladdingSystem;

public class DeleteCladdingSystemHandler : IRequestHandler<DeleteCladdingSystemRequest, Unit>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public DeleteCladdingSystemHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
    {
        _dbConnectionWrapper = dbConnectionWrapper;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<Unit> Handle(DeleteCladdingSystemRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId(); 
        await _dbConnectionWrapper
            .ExecuteAsync("DeleteCladdingSystem", 
                new
                {
                    ApplicationId = applicationId,
                    request.FireRiskCladdingSystemsId
                });
        return Unit.Value;
    }
}