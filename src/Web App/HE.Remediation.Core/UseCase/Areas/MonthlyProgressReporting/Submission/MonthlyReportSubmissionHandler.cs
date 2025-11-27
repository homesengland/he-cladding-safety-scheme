using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Submission;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Transactions;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Submission;

public class MonthlyReportSubmissionHandler : IRequestHandler<MonthlyReportSubmissionRequest, MonthlyReportSubmissionResponse>
{
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IMonthlyProgressReportingRepository _monthlyProgressReportingRepository;
    private readonly IProgressReportingKeyDatesRepository _progressReportingKeyDatesRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IDateRepository _dateRepository;
    private readonly ISupportTicketRepository _supportTicketRepository;
    private readonly IProgressReportingProjectTeamRepository _projectTeamRepository;
    private readonly ILogger<MonthlyReportSubmissionHandler> _logger;

    private static readonly IDictionary<(string, string), GetTaskTypeResult> _TaskTypeCache = new Dictionary<(string, string), GetTaskTypeResult>();

    public MonthlyReportSubmissionHandler(
        IApplicationDetailsProvider applicationDetailsProvider, 
        IApplicationDataProvider applicationDataProvider, 
        IMonthlyProgressReportingRepository monthlyProgressReportingRepository, 
        IProgressReportingKeyDatesRepository progressReportingKeyDatesRepository, 
        ITaskRepository taskRepository, 
        IDateRepository dateRepository, 
        ISupportTicketRepository supportTicketRepository, 
        IProgressReportingProjectTeamRepository projectTeamRepository, 
        ILogger<MonthlyReportSubmissionHandler> logger)
    {
        _applicationDetailsProvider = applicationDetailsProvider;
        _applicationDataProvider = applicationDataProvider;
        _monthlyProgressReportingRepository = monthlyProgressReportingRepository;
        _progressReportingKeyDatesRepository = progressReportingKeyDatesRepository;
        _taskRepository = taskRepository;
        _dateRepository = dateRepository;
        _supportTicketRepository = supportTicketRepository;
        _projectTeamRepository = projectTeamRepository;
        _logger = logger;
    }

    public async Task<MonthlyReportSubmissionResponse> Handle(MonthlyReportSubmissionRequest request, CancellationToken cancellationToken)
    {
        var appDetails = await _applicationDetailsProvider.GetApplicationDetails();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var applicationId = _applicationDataProvider.GetApplicationId();
        var userId = _applicationDataProvider.GetUserId();

        if (request.IsConfirmed == true)
        {
            try
            {
                using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

                var parameters = new SetAsSubmittedParameters { ProgressReportId = progressReportId, UserId = userId };
                await _monthlyProgressReportingRepository.SetAsSubmitted(parameters);
                
                await RaiseTasks(applicationId, progressReportId);
                
                scope.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during monthly report submission transaction. ApplicationId: {ApplicationId}, ProgressReportId: {ProgressReportId}", applicationId, progressReportId);
                throw;
            }
        }

        return new MonthlyReportSubmissionResponse
        {
            ApplicationReferenceNumber = appDetails.ApplicationReferenceNumber,
            BuildingName = appDetails.BuildingName
        };
    }

    private async Task RaiseTasks(Guid applicationId, Guid progressReportId)
    {
        var nextWorkingDay = await _dateRepository.AddWorkingDays(new AddWorkingDaysParameters { Date = DateTime.UtcNow, WorkingDays = 1 });

        var taskData = await _monthlyProgressReportingRepository.GetProgressReportDataForTasks(
            new GetProgressReportDataForTasksParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        var keyDatesChangeFlags = await _progressReportingKeyDatesRepository.GetProgressReportKeyDatesChangeFlags(
            new GetMonthlyProgressReportParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        if (keyDatesChangeFlags?.HasAnyDateChanges() == true)
        {
            await RaiseDatesChangedTask(applicationId, progressReportId, nextWorkingDay);
        }

        if (taskData?.EnoughFunds == false)
        {
            await RaiseNotEnoughPtsFundsTask(applicationId, progressReportId, nextWorkingDay);
        }

        if (taskData?.HasSupportNeeds == true)
        {
            await RaiseSupportNeededTask(applicationId, progressReportId, taskData);
        }

        if (taskData?.HasNineMonthGap == true)
        {
            await RaiseNineMonthGapTask(applicationId, progressReportId, nextWorkingDay);
        }

        if (taskData?.ContractorTenderTypeId == EContractorTenderType.NonCompetitive)
        {
            await RaiseNonCompetitiveTenderTask(applicationId, progressReportId, nextWorkingDay);
        }

        if (taskData is { HasGco: true, DutyOfCareDeedTaskRaised: false })
        {
            await RaiseDutyOfCareDeedTask(applicationId, progressReportId);
        }
    }

    private async Task RaiseDatesChangedTask(Guid applicationId, Guid progressReportId, DateTime nextWorkingDay)
    {
        var taskType = await GetTaskType("Progress Report", "Review", applicationId, progressReportId);
        
        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            ReferenceId = applicationId,
            AssignedToTeamId = (int)ETeam.DaviesOps,
            AssignedToUserId = null,
            CreatedByUserId = null,
            Description = "Date slippage has occurred against Project Key Dates. Please ensure these dates are reviewed",
            RequiredByDate = DateOnly.FromDateTime(nextWorkingDay),
            TaskStatus = nameof(ETaskStatus.NotStarted),
            TaskTypeId = taskType.Id
        });
    }

    private async Task RaiseNotEnoughPtsFundsTask(
        Guid applicationId,
        Guid progressReportId,
        DateTime nextWorkingDay)
    {
        var taskType = await GetTaskType("Progress Report", "Review", applicationId, progressReportId);
    
        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            ReferenceId = applicationId,
            AssignedToTeamId = (int)ETeam.DaviesOps,
            AssignedToUserId = null,
            CreatedByUserId = null,
            Description = "Applicant advised that they do not have enough PTS funds remaining. Please review the application. The applicant may have provided their PTS uplift document in the submission.",
            RequiredByDate = DateOnly.FromDateTime(nextWorkingDay),
            TaskStatus = nameof(ETaskStatus.NotStarted),
            TaskTypeId = taskType.Id
        });
        
    }

    private async Task RaiseSupportNeededTask(Guid applicationId, Guid progressReportId, GetProgressReportDataForTasksResult taskData)
    {
        var taskType = await GetTaskType("Progress Report", "Support request", applicationId, progressReportId);

        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            ReferenceId = applicationId,
            AssignedToTeamId = (int)ETeam.DaviesOps,
            Description = "Support requested for following areas: " +
                          $"{(taskData.LeadDesignerNeedsSupport == true ? "Lead designer, " : string.Empty)}" +
                          $"{(taskData.OtherMembersNeedsSupport == true ? "Other members, " : string.Empty)}" +
                          $"{(taskData.QuotesNeedsSupport == true ? "Quotes, " : string.Empty)}" +
                          $"{(taskData.PlanningPermissionNeedsSupport == true ? "Planning permission, " : string.Empty)}" +
                          $"{(taskData.OtherNeedsSupport == true ? "Other, " : string.Empty)}reason supplied by applicant: {taskData.SupportNeededReason}.",
            RequiredByDate = DateOnly.FromDateTime(DateTime.Today),
            TaskStatus = nameof(ETaskStatus.NotStarted),
            TaskTypeId = taskType.Id,
            Notes = null
        });

        var supportTicketId = await _supportTicketRepository.InsertSupportTicket(new InsertSupportTicketParameters
        {
            ApplicationId = applicationId,
            SupportTicketTypeId = (int)ESupportTicketType.ProgressReport,
            Description = taskData.SupportNeededReason
        });

        var supportAreas = GetSupportAreas(taskData, supportTicketId);
        foreach (var area in supportAreas)
        {
            await _supportTicketRepository.InsertSupportTicketArea(area);
        }
    }

    private async Task RaiseNineMonthGapTask(Guid applicationId, Guid progressReportId, DateTime nextWorkingDay)
    {
        var taskType = await GetTaskType("Progress Report", "Contact Applicant", applicationId, progressReportId);
        
        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            ReferenceId = applicationId,
            AssignedToTeamId = (int)ETeam.DaviesOps,
            AssignedToUserId = null,
            CreatedByUserId = null,
            Description = "Please contact the applicant as forecast Works package submission date is longer than 9 months after GFA",
            RequiredByDate = DateOnly.FromDateTime(nextWorkingDay),
            TaskStatus = nameof(ETaskStatus.NotStarted),
            TaskTypeId = taskType.Id,
            TopicId = taskType.TopicId
        });
    }

    private async Task RaiseNonCompetitiveTenderTask(Guid applicationId, Guid progressReportId, DateTime nextWorkingDay)
    {
        var taskType = await GetTaskType("Progress Report", "Review", applicationId, progressReportId);

        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            ReferenceId = applicationId,
            AssignedToTeamId = (int)ETeam.DaviesOps,
            AssignedToUserId = null,
            CreatedByUserId = null,
            Description = "Applicant is running a non-competitive tender for their contractor. Please review the application.",
            RequiredByDate = DateOnly.FromDateTime(nextWorkingDay),
            TaskStatus = nameof(ETaskStatus.NotStarted),
            TaskTypeId = taskType.Id,
            TopicId = taskType.TopicId
        });
    }

    private async Task RaiseDutyOfCareDeedTask(Guid applicationId, Guid progressReportId)
    {
        var taskType = await GetTaskType("Progress Report", "Progress Report GCO Duty of care deed", applicationId, progressReportId);

        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            ReferenceId = _applicationDataProvider.GetApplicationId(),
            AssignedToTeamId = (int)ETeam.DaviesOps,
            Description = "Send Duty of care deed to Grant Certifying Officer",
            RequiredByDate = DateOnly.FromDateTime(DateTime.Today),
            TaskStatus = nameof(ETaskStatus.NotStarted),
            TaskTypeId = taskType.Id,
            Notes = null
        });

        await _projectTeamRepository.UpdateGrantCertifyingOfficerDutyOfCareDeedTaskRaised(
            new UpdateGrantCertifyingOfficerDutyOfCareDeedTaskRaisedParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                DutyOfCareDeedTaskRaised = true
            });
    }

    private static IEnumerable<InsertSupportTicketAreaParameters> GetSupportAreas(GetProgressReportDataForTasksResult supportNeeds, Guid supportTicketId)
    {
        var areas = new List<InsertSupportTicketAreaParameters>(4);

        if (supportNeeds.LeadDesignerNeedsSupport == true)
        {
            areas.Add(new InsertSupportTicketAreaParameters
            {
                Description = null,
                SupportTicketAreaTypeId = (int)ESupportTicketAreaType.LeadDesigner,
                SupportTicketId = supportTicketId
            });
        }

        if (supportNeeds.OtherMembersNeedsSupport == true)
        {
            areas.Add(new InsertSupportTicketAreaParameters
            {
                Description = null,
                SupportTicketAreaTypeId = (int)ESupportTicketAreaType.OtherMembers,
                SupportTicketId = supportTicketId
            });
        }

        if (supportNeeds.QuotesNeedsSupport == true)
        {
            areas.Add(new InsertSupportTicketAreaParameters
            {
                Description = null,
                SupportTicketAreaTypeId = (int)ESupportTicketAreaType.Quotes,
                SupportTicketId = supportTicketId
            });
        }

        if (supportNeeds.PlanningPermissionNeedsSupport == true)
        {
            areas.Add(new InsertSupportTicketAreaParameters
            {
                Description = null,
                SupportTicketAreaTypeId = (int)ESupportTicketAreaType.PlanningPermission,
                SupportTicketId = supportTicketId
            });
        }
        
        return areas;
    }

    private async Task<GetTaskTypeResult> GetTaskType(string parentType, string childType, Guid applicationId, Guid progressReportId)
    {
        var key = (parentType, childType);
        if (_TaskTypeCache.TryGetValue(key, out var type))
        {
            return type;
        }

        type = await _taskRepository.GetTaskType(new GetTaskTypeParameters(parentType, childType));
        if (type is null)
        {
            _logger.LogError("Task type not found for {ParentType} - {ChildType}. ApplicationId: {ApplicationId}, ProgressReportId: {ProgressReportId}", parentType, childType, applicationId, progressReportId);
            throw new InvalidOperationException($"Task type not found for {parentType} - {childType}");
        }
        _TaskTypeCache.Add(key, type);
        return type;
    }
}

public class MonthlyReportSubmissionRequest() : IRequest<MonthlyReportSubmissionResponse>
{
    public bool? IsConfirmed { get; set; }
}

public class MonthlyReportSubmissionResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
}
