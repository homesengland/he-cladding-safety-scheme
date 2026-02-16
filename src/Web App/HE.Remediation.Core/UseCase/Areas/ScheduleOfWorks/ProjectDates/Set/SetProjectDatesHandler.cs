using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.ScheduleOfWorks;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ProjectDates.Set;

public class SetProjectDatesHandler : IRequestHandler<SetProjectDatesRequest>
{
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public SetProjectDatesHandler(IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async ValueTask<Unit> Handle(SetProjectDatesRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _scheduleOfWorksRepository.UpdateProjectDates(new UpdateProjectDatesParameters
        {
            ProjectStartDate = GetStartDate(request.ProjectStartDateMonth, request.ProjectStartDateYear),
            ProjectEndDate = GetEndDate(request.ProjectEndDateMonth, request.ProjectEndDateYear)
        });

        scope.Complete();

        return Unit.Value;
    }

    private DateTime? GetStartDate(int? month, int? year)
    {
        return month is not null && year is not null
            ? new DateTime(year.Value, month.Value, 1)
            : null;
    }

    private DateTime? GetEndDate(int? month, int? year)
    {
        return month is not null && year is not null
            ? new DateTime(year.Value, month.Value, 1).AddMonths(1).AddDays(-1)
            : null;
    }
}