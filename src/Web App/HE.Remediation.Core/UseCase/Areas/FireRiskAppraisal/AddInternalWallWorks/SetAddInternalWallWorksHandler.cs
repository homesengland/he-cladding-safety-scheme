using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddExternalWallWorks;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddInternalWallWorks;

public class SetAddInternalWallWorksHandler: IRequestHandler<SetAddInternalWallWorksRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskWorksRepository _fireRiskWorksRepository;

    public SetAddInternalWallWorksHandler(IApplicationDataProvider applicationDataProvider, 
                                          IFireRiskWorksRepository fireRiskWorksRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskWorksRepository = fireRiskWorksRepository;
    }

    public async Task<Unit> Handle(SetAddInternalWallWorksRequest request, CancellationToken cancellationToken)
    {
        if (request.Id is null)
        {
            await InsertInternalWallWorks(request);
        }
        else
        {
            await UpdateExternalWallWorks(request);
        }
        
        return Unit.Value;
    }

    private async Task InsertInternalWallWorks(SetAddInternalWallWorksRequest request)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        await _fireRiskWorksRepository.InsertWallWorks(Enums.EWorkType.Internal, request.Description, applicationId);
    }

    private async Task UpdateExternalWallWorks(SetAddInternalWallWorksRequest  request)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        await _fireRiskWorksRepository.UpdateWallWorks(Enums.EWorkType.Internal, request.Description, applicationId, request.Id.Value);
    }
}
