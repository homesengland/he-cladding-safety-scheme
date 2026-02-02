using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWallWorks;

public class GetExternalWallWorksHandler : IRequestHandler<GetExternalWallWorksRequest, List<GetWallWorksListResult>>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskWorksRepository _fireRiskWorksRepository;
        
    public GetExternalWallWorksHandler(IDbConnectionWrapper dbConnectionWrapper, 
                                       IApplicationDataProvider applicationDataProvider,
                                       IFireRiskWorksRepository fireRiskWorksRepository)
    {
        _dbConnectionWrapper = dbConnectionWrapper;
        _applicationDataProvider = applicationDataProvider;
        _fireRiskWorksRepository = fireRiskWorksRepository;
    }

    public async ValueTask<List<GetWallWorksListResult>> Handle(GetExternalWallWorksRequest request, CancellationToken cancellationToken)
    {
        return await GetExternalWallWorks();
    }

    private async ValueTask<List<GetWallWorksListResult>> GetExternalWallWorks()
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        return await _fireRiskWorksRepository.GetFireRiskWallWorks(_applicationDataProvider.GetApplicationId(),
                                                                          EWorkType.External);
    }
}
