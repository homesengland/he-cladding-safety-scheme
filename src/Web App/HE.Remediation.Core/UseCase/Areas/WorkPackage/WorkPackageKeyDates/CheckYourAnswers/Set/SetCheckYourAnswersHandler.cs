using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.CheckYourAnswers.Set;

public class SetCheckYourAnswersHandler : IRequestHandler<SetCheckYourAnswersRequest, Unit>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetCheckYourAnswersHandler(
        IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(SetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var taskStatus = await _workPackageRepository.GetKeyDatesStatus();

        if (taskStatus != ETaskStatus.Completed)
        {
            await _workPackageRepository.UpdateKeyDatesStatus(ETaskStatus.Completed);
        }

        return Unit.Value;
    }
}
