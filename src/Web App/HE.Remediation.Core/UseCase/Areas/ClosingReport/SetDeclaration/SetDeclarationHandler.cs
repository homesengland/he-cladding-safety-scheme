using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Communication;
using Mediator;
using System.Transactions;
using HE.Remediation.Core.Services.StatusTransition;
using HE.Remediation.Core.Data.StoredProcedureParameters;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetDeclaration;

public class SetDeclarationHandler : IRequestHandler<SetDeclarationRequest>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IClosingReportRepository _closingReportRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly ICommunicationService _communicationService;
    private readonly IStatusTransitionService _statusTransitionService;
    private readonly ITaskRepository _taskRepository;
    private readonly IDateRepository _dateRepository;

    public SetDeclarationHandler(
        IApplicationDataProvider adp,
        IClosingReportRepository closingReportRepository,
        IApplicationRepository applicationRepository,
        ICommunicationService communicationService, 
        IStatusTransitionService statusTransitionService,
        ITaskRepository taskRepository,
        IDateRepository dateRepository)
    {
        _adp = adp;
        _closingReportRepository = closingReportRepository;
        _applicationRepository = applicationRepository;
        _communicationService = communicationService;
        _statusTransitionService = statusTransitionService;
        _taskRepository = taskRepository;
        _dateRepository = dateRepository;
    }

    public async ValueTask<Unit> Handle(SetDeclarationRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _adp.GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _closingReportRepository.UpdateClosingReportProjectDate(applicationId, request.DateOfCompletion);
        await _closingReportRepository.UpdateClosingReportDeclarations(applicationId, request.FraewRiskToLifeReduced, request.GrantFundingObligations);
        await _closingReportRepository.UpdateClosingReportToSubmitted(applicationId);

        await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.ClosingReportSubmitted, applicationIds: applicationId);
        await _applicationRepository.UpdateApplicationStage(applicationId, EApplicationStage.WorksCompleted);

        await _communicationService.QueueEmailCommunication(new EmailCommunicationRequest
        (
            applicationId,
            EEmailType.ClosingReportSubmitted
        ));

        await _closingReportRepository.UpsertClosingReportTaskStatus(applicationId, EClosingReportTask.FinalPaymentDeclaration, ETaskStatus.Completed);

        await SendClosingPaymentRequestChecks(applicationId);

        scope.Complete();

        return Unit.Value;
    }

    private async Task SendClosingPaymentRequestChecks(Guid applicationId)
    {
        var taskType = await _taskRepository.GetTaskType(
            new GetTaskTypeParameters("Closing payment request checks", "Review closing payment request")
        );
        var dueDate = await _dateRepository.AddWorkingDays(new AddWorkingDaysParameters
        {
            Date = DateTime.UtcNow.Date,
            WorkingDays = 5
        });
        await _taskRepository.InsertTask(new InsertTaskParameters
        {
            ReferenceId = applicationId,
            AssignedToTeamId = (int)ETeam.DaviesOps,
            AssignedToUserId = null,
            CreatedByUserId = null,
            Description =
                $"Please review the closing payment request and provide a recommendation for HE whether to:{Environment.NewLine}•Approve the request{Environment.NewLine}•Reject the request{Environment.NewLine}•Approve reduced amount{Environment.NewLine}All exit checks must be undertaken as part of this review.",
            RequiredByDate = DateOnly.FromDateTime(dueDate),
            TaskStatus = ETaskStatus.NotStarted.ToString(),
            TopicId = taskType.TopicId,
            TaskTypeId = taskType.Id
        });
    }
}
