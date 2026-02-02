using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.DeleteCladdingSystem;

public class DeleteCladdingSystemHandler : IRequestHandler<DeleteCladdingSystemRequest, Unit>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskWorksRepository _fireRiskWorksRepository;
    private readonly IFireRiskAppraisalRepository _fireRiskAppraisalRepository;

    public DeleteCladdingSystemHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider, IFireRiskWorksRepository fireRiskWorksRepository, IFireRiskAppraisalRepository fireRiskAppraisalRepository)
    {
        _dbConnectionWrapper = dbConnectionWrapper;
        _applicationDataProvider = applicationDataProvider;
        _fireRiskWorksRepository = fireRiskWorksRepository;
        _fireRiskAppraisalRepository = fireRiskAppraisalRepository;
    }

    public async ValueTask<Unit> Handle(DeleteCladdingSystemRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId(); 
        await _dbConnectionWrapper
            .ExecuteAsync("DeleteCladdingSystem", 
                new
                {
                    ApplicationId = applicationId,
                    request.FireRiskCladdingSystemsId
                });

        var claddingSystems = await _fireRiskWorksRepository.GetFireRiskCladdingWorks(applicationId);

        if (claddingSystems.Count == 0)
            await _fireRiskAppraisalRepository.UpdateStatusToInProgress();

        return Unit.Value;
    }
}