using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.CheckYourAnswers.Set;

public class SetCheckYourAnswersHandler : IRequestHandler<SetCheckYourAnswersRequest, Unit>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetCheckYourAnswersHandler(
        IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var taskStatus = await _workPackageRepository.GetThirdPartyContributionsStatus();

        if (taskStatus != ETaskStatus.Completed)
        {
            await _workPackageRepository.UpdateThirdPartyContributionsStatus(ETaskStatus.Completed);
        }

        return Unit.Value;
    }
}
