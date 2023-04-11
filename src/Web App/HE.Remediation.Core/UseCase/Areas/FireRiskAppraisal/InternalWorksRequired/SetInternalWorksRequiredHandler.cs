using HE.Remediation.Core.Data.Repositories;
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
        var applicaitonId = _applicationDataProvider.GetApplicationId();

        await _fireRiskWorksRepository.SetInternalWorksRequired(applicaitonId, request.WorkRequired);

        return Unit.Value;
    }
}
