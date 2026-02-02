using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PracticalCompletionMilestone;

public class UpdatePracticalCompletionHandler : IRequestHandler<UpdatePracticalCompletionRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IMilestoneRepository _milestoneRepository;

    public UpdatePracticalCompletionHandler(IApplicationDataProvider applicationDataProvider, IMilestoneRepository milestoneRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _milestoneRepository = milestoneRepository;
    }

    public async ValueTask<Unit> Handle(UpdatePracticalCompletionRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        await _milestoneRepository.UpdatePracticalCompletionMilestone(new UpdatePracticalCompletionMilestoneParameters
        {
            ApplicationId = applicationId,
            PracticalCompletionDate = request.PracticalCompletionDate!.Value
        });

        return Unit.Value;
    }
}

public class UpdatePracticalCompletionRequest : IRequest
{
    public DateTime? PracticalCompletionDate { get; set; }
}