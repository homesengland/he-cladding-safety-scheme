using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.NotEligible.SetNotEligible;

public class SetNotEligibleHandler : IRequestHandler<SetNotEligibleRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IStatusTransitionService _statusTransitionService;

    public SetNotEligibleHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IStatusTransitionService statusTransitionService)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _statusTransitionService = statusTransitionService;
    }

    public async Task<Unit> Handle(SetNotEligibleRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        await _applicationRepository.UpdateApplicationStage(applicationId, EApplicationStage.Closed);

        await _statusTransitionService.TransitionToStatus(EApplicationStatus.ApplicationNotEligible,
            "Applicant must be registered in the UK to apply for the remediation grant.", applicationId);

        return Unit.Value;
    }
}