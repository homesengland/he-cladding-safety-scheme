using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.StartedOnSiteMilestone;

public class UpdateStartedOnSiteHandler : IRequestHandler<UpdateStartedOnSiteRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IMilestoneRepository _milestoneRepository;

    public UpdateStartedOnSiteHandler(
        IApplicationDataProvider applicationDataProvider, 
        IMilestoneRepository milestoneRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _milestoneRepository = milestoneRepository;
    }

    public async ValueTask<Unit> Handle(UpdateStartedOnSiteRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        await _milestoneRepository.UpdateStartOnSiteMilestone(new UpdateStartOnSiteMilestoneParameters
        {
            ApplicationId = applicationId,
            StartOnSiteDate = request.StartedOnSiteDate!.Value
        });

        return Unit.Value;
    }
}

public class UpdateStartedOnSiteRequest : IRequest
{
    public DateTime? StartedOnSiteDate { get; set; }
}