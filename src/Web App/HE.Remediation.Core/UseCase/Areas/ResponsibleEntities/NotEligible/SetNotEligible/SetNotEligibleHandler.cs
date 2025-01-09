using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.NotEligible.SetNotEligible;

public class SetNotEligibleHandler : IRequestHandler<SetNotEligibleRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;

    public SetNotEligibleHandler(IApplicationDataProvider applicationDataProvider, IApplicationRepository applicationRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
    }

    public async Task<Unit> Handle(SetNotEligibleRequest request, CancellationToken cancellationToken)
    {
        await _applicationRepository.UpdateApplicationStage(_applicationDataProvider.GetApplicationId(),
            EApplicationStage.Closed);

        await _applicationRepository.UpdateStatus(_applicationDataProvider.GetApplicationId(),
            EApplicationStatus.ApplicationNotEligible,
            "Applicant must be registered in the UK to apply for the remediation grant.");

        return Unit.Value;
    }
}