
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonQuotesNotSought.SetReasonQuotesNotSought;

public class SetReasonQuotesNotSoughtHandler : IRequestHandler<SetReasonQuotesNotSoughtRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly ITaskRepository _taskRepository;
    private readonly IDateRepository _dateRepository;

    public SetReasonQuotesNotSoughtHandler(IProgressReportingRepository progressReportingRepository,
        IApplicationDataProvider applicationDataProvider,
        ITaskRepository taskRepository,
        IDateRepository dateRepository)
    {
        _progressReportingRepository = progressReportingRepository;
        _applicationDataProvider = applicationDataProvider;
        _taskRepository = taskRepository;
        _dateRepository = dateRepository;
    }

    public async ValueTask<Unit> Handle(SetReasonQuotesNotSoughtRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        await _progressReportingRepository.UpdateQuotesNotSoughtReason(request.WhyYouHaveNotSoughtQuotes, request.QuotesNotSoughtReason, request.QuotesNeedsSupport);

        var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters("Progress Report", "Review"));

        // next working day - weekends and holidays are in DateReference table
        var dueDate = await _dateRepository.AddWorkingDays(new AddWorkingDaysParameters
        {
            Date = DateTime.UtcNow.Date,
            WorkingDays = 1
        });

        if (request.WhyYouHaveNotSoughtQuotes == EWhyYouHaveNotSoughtQuotes.IDontPlanTo)
        {
            await _taskRepository.InsertTask(new InsertTaskParameters
            {
                ReferenceId = applicationId,
                AssignedToTeamId = (int)ETeam.DaviesOps,
                AssignedToUserId = null,
                CreatedByUserId = null,
                Description = "Please contact applicant as not planning on running open tender",
                RequiredByDate = DateOnly.FromDateTime(dueDate),
                TaskStatus = ETaskStatus.NotStarted.ToString(),
                TaskTypeId = taskType.Id
            });
        }

        return Unit.Value;
    }
}
