using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetSubmitPayment;

public class SetSubmitPaymentHandler : IRequestHandler<SetSubmitPaymentRequest, SetSubmitPaymentResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IClosingReportRepository _closingReportRepository;

    public SetSubmitPaymentHandler(IClosingReportRepository closingReportRepository,
        IApplicationDataProvider applicationDataProvider, 
        ITaskRepository taskRepository,
        IDateRepository dateRepository)
    {
        _closingReportRepository = closingReportRepository;
        _applicationDataProvider = applicationDataProvider;
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

        scope.Complete();

        return response;
    } 

    
    public static bool HasCurrentCostChanged(decimal originalCost, 
                                      decimal? newCosts)
    {
        if (newCosts == null)
        {
            return false;
        }

        return (newCosts != originalCost);    
    }

    private static SetSubmitPaymentResponse ValidateSubmitPaymentRequest(SetSubmitPaymentRequest request, decimal allowedFinalPaymentAmount)
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
