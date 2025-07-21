using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.SetWhenSubmit;

public class SetWhenSubmitHandler : IRequestHandler<SetWhenSubmitRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IDateRepository _dateRepository;
    private readonly IGrantFundingRepository _grantFundingRepository;

    public SetWhenSubmitHandler(
        IProgressReportingRepository progressReportingRepository, 
        ITaskRepository taskRepository, 
        IDateRepository dateRepository,
        IGrantFundingRepository grantFundingRepository,
        IApplicationDataProvider applicationDataProvider)
    {
        _progressReportingRepository = progressReportingRepository;
        _taskRepository = taskRepository;
        _applicationDataProvider = applicationDataProvider;
        _dateRepository = dateRepository;
        _grantFundingRepository = grantFundingRepository;
    }

    public async Task<Unit> Handle(SetWhenSubmitRequest request, CancellationToken cancellationToken)
    {
        var forecastRoundedDate = ToMonthEnd(request.SubmissionMonth, request.SubmissionYear);
        await CreateTaskIfNineMonthGap(forecastRoundedDate);
        await _progressReportingRepository.UpdateProgressReportExpectedWorksPackageSubmissionDate(forecastRoundedDate);

        return Unit.Value;
    }

    private async Task CreateTaskIfNineMonthGap(DateTime? forecastRoundedDate)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var gfaCompletionDate = await _grantFundingRepository.GetGrantFundingAgreementCompleteDate(applicationId);
        var gfaCompletionRoundedDate = ToMonthEnd(gfaCompletionDate?.Month, gfaCompletionDate?.Year);
        if (gfaCompletionRoundedDate == null || forecastRoundedDate == null) return;
        var deadline = gfaCompletionRoundedDate.Value.AddMonths(9);
        if (forecastRoundedDate < deadline)
        {
            return;
        }
        
        var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters("Progress Report", "Contact Applicant"));
        var nextWorkingDay = await _dateRepository.AddWorkingDays(new AddWorkingDaysParameters { Date = DateTime.UtcNow, WorkingDays = 1 });
        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            ReferenceId = applicationId,
            AssignedToTeamId = (int)ETeam.DaviesOps,
            AssignedToUserId = null,
            CreatedByUserId = null,
            Description = "Please contact the applicant as forecast Works package submission date is longer than 9 months after GFA",
            RequiredByDate = DateOnly.FromDateTime(nextWorkingDay),
            TaskStatus = ETaskStatus.NotStarted.ToString(),
            TaskTypeId = taskType.Id,
            TopicId = taskType.TopicId
        });
    }

    private static DateTime? ToMonthEnd(int? month, int? year)
    {
        return month is not null && year is not null
            ? new DateTime(year.Value, month.Value, 1).AddMonths(1).AddDays(-1)
        : null;
    }
}
