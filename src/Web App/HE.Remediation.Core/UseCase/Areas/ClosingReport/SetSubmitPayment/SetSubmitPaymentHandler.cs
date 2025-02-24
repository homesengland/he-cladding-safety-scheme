using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetSubmitPayment;

public class SetSubmitPaymentHandler : IRequestHandler<SetSubmitPaymentRequest, SetSubmitPaymentResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IClosingReportRepository _closingReportRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IDateRepository _dateRepository;

    public SetSubmitPaymentHandler(IClosingReportRepository closingReportRepository,
        IApplicationDataProvider applicationDataProvider, 
        ITaskRepository taskRepository,
        IDateRepository dateRepository)
    {
        _closingReportRepository = closingReportRepository;
        _applicationDataProvider = applicationDataProvider;
        _taskRepository = taskRepository;
        _dateRepository = dateRepository;
    }

    public async Task<SetSubmitPaymentResponse> Handle(SetSubmitPaymentRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var allowedFinalPaymentAmount =
            await _closingReportRepository.GetClosingReportAllowedFinalPaymentAmount(applicationId);

        var response = ValidateSubmitPaymentRequest(request, allowedFinalPaymentAmount);

        if (!response.IsValidRequest) return response;

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var existingCost = await _closingReportRepository.GetClosingReportScheduledAmount(applicationId);
        var haveCostsChanged = HasCurrentCostChanged(existingCost, request.FinalMonthCost.Amount);

        await _closingReportRepository.UpdateFinalPaymentAmount(request.FinalMonthCost.Id,
            request.FinalMonthCost.Amount);

        await _closingReportRepository.UpdateClosingReportCostChanged(applicationId, haveCostsChanged);


        var taskType = await _taskRepository.GetTaskType(new GetTaskTypeParameters("Closing payment request checks",
            "Review closing payment request"));
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

        scope.Complete();

        return response;
    } 

    
    public bool HasCurrentCostChanged(decimal originalCost, 
                                      decimal? newCosts)
    {
        if (newCosts == null)
        {
            return false;
        }

        return (newCosts != originalCost);    
    }

    private SetSubmitPaymentResponse ValidateSubmitPaymentRequest(SetSubmitPaymentRequest request, decimal allowedFinalPaymentAmount)
    {
        var validationMessage = string.Empty;
        var validRequest = request.FinalMonthCost.Amount <= allowedFinalPaymentAmount;
        if (!validRequest)
        {
            validationMessage =
                "You have over-allocated your funding. Please update your final cost to be equal to or below the final 5% payment";
        }

        return new SetSubmitPaymentResponse
        {
            IsValidRequest = validRequest,
            ValidationMessage = validationMessage
        };
    }
}
