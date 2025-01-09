
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

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

    public async Task<Unit> Handle(SetReasonQuotesNotSoughtRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        await UpdateQuotesNotSoughtReason(request);

        var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters("Progress Report",
            "Review"));

        if (request.WhyYouHaveNotSoughtQuotes == EWhyYouHaveNotSoughtQuotes.IDontPlanToo)
        {
            await _taskRepository.InsertTask(new InsertTaskParameters
            {
                ReferenceId = applicationId,
                AssignedToTeamId = (int)ETeam.DaviesOps,
                AssignedToUserId = null,
                CreatedByUserId = null,
                Description = "Please review reason for not seeking quotes or issuing a tender",
                RequiredByDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(5)),
                TaskStatus = ETaskStatus.NotStarted.ToString(),
                TaskTypeId = taskType.Id
            });
        }

        return Unit.Value;
    }

    private async Task UpdateQuotesNotSoughtReason(SetReasonQuotesNotSoughtRequest request)
    {
        await _progressReportingRepository.UpdateQuotesNotSoughtReason(request.WhyYouHaveNotSoughtQuotes, request.QuotesNotSoughtReason, request.QuotesNeedsSupport);
    }
}
