using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InternalWorksRequired;

public class SetInternalWorksRequiredHandler: IRequestHandler<SetInternalWorksRequiredRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskWorksRepository _fireRiskWorksRepository;

    public SetInternalWorksRequiredHandler(IApplicationDataProvider applicationDataProvider, IFireRiskWorksRepository fireRiskWorksRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskWorksRepository = fireRiskWorksRepository;
    }

    public async Task<Unit> Handle(SetInternalWorksRequiredRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        await _fireRiskWorksRepository.SetInternalWorksRequired(applicationId, request.WorkRequired);

        if (request.WorkRequired == ENoYes.No)
        {
            await _fireRiskWorksRepository.ResetFireRiskWallWorks(applicationId, EWorkType.Internal);
        }

        return Unit.Value;
    }
}
