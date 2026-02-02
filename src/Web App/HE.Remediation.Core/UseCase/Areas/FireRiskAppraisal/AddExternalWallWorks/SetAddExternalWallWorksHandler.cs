
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddExternalWallWorks;

public class SetAddExternalWallWorksHandler : IRequestHandler<SetAddExternalWallWorksRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskWorksRepository _fireRiskWorksRepository;

    public SetAddExternalWallWorksHandler(IApplicationDataProvider applicationDataProvider, 
                                          IFireRiskWorksRepository fireRiskWorksRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskWorksRepository = fireRiskWorksRepository;
    }

    public async ValueTask<Unit> Handle(SetAddExternalWallWorksRequest request, CancellationToken cancellationToken)
    {
        if (request.Id is null)
        {
            await InsertExternalWallWorks(request);
        }
        else
        {
            await UpdateExternalWallWorks(request);
        }
        
        return Unit.Value;
    }

    private async Task InsertExternalWallWorks(SetAddExternalWallWorksRequest request)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        await _fireRiskWorksRepository.InsertWallWorks(Enums.EWorkType.External, request.Description, applicationId);
    }

    private async Task UpdateExternalWallWorks(SetAddExternalWallWorksRequest request)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        await _fireRiskWorksRepository.UpdateWallWorks(Enums.EWorkType.External, request.Description, applicationId, request.Id.Value);
    }
}
