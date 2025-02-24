
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWallWorks;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InternalWallWorks;

public class GetInternalWallWorksHandler: IRequestHandler<GetInternalWallWorksRequest, List<GetWallWorksListResult>>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskWorksRepository _fireRiskWorksRepository;
        
    public GetInternalWallWorksHandler(IDbConnectionWrapper dbConnectionWrapper, 
                                       IApplicationDataProvider applicationDataProvider,
                                       IFireRiskWorksRepository fireRiskWorksRepository)
    {
        _dbConnectionWrapper = dbConnectionWrapper;
        _applicationDataProvider = applicationDataProvider;
        _fireRiskWorksRepository = fireRiskWorksRepository;
    }

    public async Task<List<GetWallWorksListResult>> Handle(GetInternalWallWorksRequest request, CancellationToken cancellationToken)
    {
        return await GetInternalWallWorks();
    }

    private async Task<List<GetWallWorksListResult>> GetInternalWallWorks()
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        return await _fireRiskWorksRepository.GetFireRiskWallWorks(_applicationDataProvider.GetApplicationId(),
                                                                          EWorkType.Internal);
    }
}
