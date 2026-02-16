using Mediator;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using System.Transactions;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Services.StatusTransition;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.FinalCheckYourAnswers.SetFinalCheckYourAnswers;

public class SetFinalCheckYourAnswersHandler : IRequestHandler<SetFinalCheckYourAnswersRequest, Unit>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly ISupportTicketRepository _supportTicketRepository;
    private readonly IStatusTransitionService _statusTransitionService;
    private readonly IAlertRepository _alertRepository;

    public SetFinalCheckYourAnswersHandler(
            IDateTimeProvider dateTimeProvider,
            IProgressReportingRepository progressReportingRepository,
            ITaskRepository taskRepository,
            IApplicationDataProvider applicationDataProvider,
            ISupportTicketRepository supportTicketRepository, 
            IStatusTransitionService statusTransitionService,
            IAlertRepository alertRepository)
    {
        _progressReportingRepository = progressReportingRepository;
        _dateTimeProvider = dateTimeProvider;
        _taskRepository = taskRepository;
        _applicationDataProvider = applicationDataProvider;
        _supportTicketRepository = supportTicketRepository;
        _statusTransitionService = statusTransitionService;
        _alertRepository = alertRepository;
    }

    public async ValueTask<Unit> Handle(SetFinalCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var userId = _applicationDataProvider.GetUserId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            
        await _progressReportingRepository.UpdateProgressReportDateSubmitted(_dateTimeProvider.UtcNow, userId);

        await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.PrimaryReportSubmitted, applicationIds: applicationId);

        await _alertRepository.InsertAlert(new InsertAlertParameters
        {
            ApplicationId = applicationId,
            AlertTypeId = (int)EAlertType.WorksPackage
        });

        var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters("Progress Report", "Support request"));
        var supportNeeds = await _progressReportingRepository.GetProgressReportSupportNeeds();

        if (taskType is not null && SupportRequired(supportNeeds))
        {
            await _taskRepository.InsertTask(new InsertTaskParameters
            {
                ReferenceId = applicationId,
                AssignedToTeamId = (int)ETeam.DaviesOps,
                Description = "Support requested for following areas:" +
                              $"{(supportNeeds.LeadDesignerNeedsSupport == true ? "Lead designer, " : string.Empty)}" +
                              $"{(supportNeeds.OtherMembersNeedsSupport == true ? "Other members, " : string.Empty)}" +
                              $"{(supportNeeds.QuotesNeedsSupport == true ? "Quotes, " : string.Empty)}" +
                              $"{(supportNeeds.PlanningPermissionNeedsSupport == true ? "Planning permission," : string.Empty)} reason supplied by applicant: {supportNeeds.SupportNeededReason}.",
                RequiredByDate = DateOnly.FromDateTime(DateTime.Today),
                TaskStatus = ETaskStatus.NotStarted.ToString(),
                TaskTypeId = taskType.Id,
                Notes = null
            });

            var supportTicketId = await _supportTicketRepository.InsertSupportTicket(new InsertSupportTicketParameters
            {
                ApplicationId = applicationId,
                SupportTicketTypeId = (int)ESupportTicketType.ProgressReport,
                Description = supportNeeds.SupportNeededReason
            });

            var supportAreas = GetSupportAreas(supportNeeds, supportTicketId);
            foreach (var area in supportAreas)
            {
                await _supportTicketRepository.InsertSupportTicketArea(area);
            }
        }

        scope.Complete();
        return Unit.Value;
    }

    private static IEnumerable<InsertSupportTicketAreaParameters> GetSupportAreas(GetProgressReportSupportNeedsResult supportNeeds, Guid supportTicketId)
    {
        var areas = new List<InsertSupportTicketAreaParameters>(4);

        if (supportNeeds.LeadDesignerNeedsSupport == true)
        {
            areas.Add(new InsertSupportTicketAreaParameters
            {
                Description = supportNeeds.LeadDesignerNotAppointedReason,
                SupportTicketAreaTypeId = (int)ESupportTicketAreaType.LeadDesigner,
                SupportTicketId = supportTicketId
            });
        }

        if (supportNeeds.OtherMembersNeedsSupport == true)
        {
            areas.Add(new InsertSupportTicketAreaParameters
            {
                Description = supportNeeds.OtherMembersNotAppointedReason,
                SupportTicketAreaTypeId = (int)ESupportTicketAreaType.OtherMembers,
                SupportTicketId = supportTicketId
            });
        }

        if (supportNeeds.QuotesNeedsSupport == true)
        {
            areas.Add(new InsertSupportTicketAreaParameters
            {
                Description = supportNeeds.QuotesNotSoughtReason,
                SupportTicketAreaTypeId = (int)ESupportTicketAreaType.Quotes,
                SupportTicketId = supportTicketId
            });
        }

        if (supportNeeds.PlanningPermissionNeedsSupport == true)
        {
            areas.Add(new InsertSupportTicketAreaParameters
            {
                Description = supportNeeds.ReasonPlanningPermissionNotApplied,
                SupportTicketAreaTypeId = (int)ESupportTicketAreaType.PlanningPermission,
                SupportTicketId = supportTicketId
            });
        }

        return areas;
    }

    private static bool SupportRequired(GetProgressReportSupportNeedsResult supportNeeds) =>
        supportNeeds.LeadDesignerNeedsSupport == true
        || supportNeeds.OtherMembersNeedsSupport == true
        || supportNeeds.QuotesNeedsSupport == true
        || supportNeeds.PlanningPermissionNeedsSupport == true
        || supportNeeds.OtherNeedsSupport == true;
}