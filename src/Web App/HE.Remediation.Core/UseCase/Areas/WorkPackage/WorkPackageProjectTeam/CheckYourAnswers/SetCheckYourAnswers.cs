using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.CheckYourAnswers;

public class SetCheckYourAnswersHandler : IRequestHandler<SetCheckYourAnswersRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetCheckYourAnswersHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var taskStatus = await _workPackageRepository.GetTeamStatus();

        if (taskStatus != ETaskStatus.Completed)
        {
            await _workPackageRepository.UpdateTeamStatus(ETaskStatus.Completed);
        }

        return Unit.Value;
    }
}

public class SetCheckYourAnswersRequest : IRequest
{
    private SetCheckYourAnswersRequest()
    {
    }

    public static readonly SetCheckYourAnswersRequest Request = new();
}