using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;

using Mediator;


namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.CheckYourAnswers.Set;


public class SetCheckYourAnswersHandler : IRequestHandler<SetCheckYourAnswersRequest>
{
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;


    public SetCheckYourAnswersHandler(IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }


    public async ValueTask<Unit> Handle(SetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();


        var isSubmitted = await _scheduleOfWorksRepository.IsScheduleOfWorksSubmitted();


        if (!isSubmitted)
        {
            var taskStatusesResult = await _scheduleOfWorksRepository.GetScheduleOfWorksTaskStatuses();


            if (taskStatusesResult?.ProfileCostsStatusId != ETaskStatus.Completed)
            {
                await _scheduleOfWorksRepository.UpdateScheduleOfWorksProfileCostsStatus(ETaskStatus.Completed);
            }
        }


        return Unit.Value;
    }
}