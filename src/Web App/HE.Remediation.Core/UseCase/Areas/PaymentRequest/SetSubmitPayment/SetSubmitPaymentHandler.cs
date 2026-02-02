using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.PaymentRequest;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;
using System.Transactions;
using HE.Remediation.Core.Services.StatusTransition;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetSubmitPayment;

public class SetSubmitPaymentHandler : IRequestHandler<SetSubmitPaymentRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IPaymentRequestRepository _paymentRequestRepository;
    private readonly IStatusTransitionService _statusTransitionService;

    public SetSubmitPaymentHandler(
        IPaymentRequestRepository paymentRequestRepository,
        IApplicationDataProvider applicationDataProvider, 
        IStatusTransitionService statusTransitionService)
    {
        _statusTransitionService = statusTransitionService;
        _paymentRequestRepository = paymentRequestRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<Unit> Handle(SetSubmitPaymentRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var costs = request.MonthlyCosts.Select(c =>
            new UpdateCostParameters
            {
                Id = c.Id,
                Date = DateTime.Parse(c.MonthName),
                Amount = c.Amount
            })
            .ToList();

        costs.Add(new UpdateCostParameters { Id = request.CurrentMonth.Id, Amount = request.CurrentMonth.Amount, Date = DateTime.Parse(request.CurrentMonth.MonthName) });

        var paymentRequest = await _paymentRequestRepository.GetPaymentRequestDetails(_applicationDataProvider.GetApplicationId(), paymentRequestId);

        var existingCost = await ObtainExistingCost();
        var costHasChanged = HasCurrentCostChanged(existingCost, costs);

        await _paymentRequestRepository.UpdatePaymentRequestUserCostChanged(paymentRequestId,
        new PaymentRequestUserCostChangedParameters
        {
            UserEnteredCostsChanged = costHasChanged
        });

        if (paymentRequest?.ScheduleOfWorksCostProfileId.HasValue == true)
        {
            await _paymentRequestRepository.UpdateScheduleOfWorksCosts(paymentRequest.ScheduleOfWorksCostProfileId!.Value, costs);
        }

        await _paymentRequestRepository.UpdatePaymentRequestAmount(paymentRequestId, request.CurrentMonth.Amount ?? 0);
        await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.PaymentRequestInProgress, applicationIds: applicationId);

        await _paymentRequestRepository.UpdatePaymentRequestTaskStatus(paymentRequestId, EPaymentRequestTaskStatus.InProgress);

        if (costHasChanged)
        {
            await _paymentRequestRepository.UpdatePaymentRequestReasonForChange(paymentRequestId, string.Empty);
        }



        scope.Complete();

        return Unit.Value;
    }

    public bool HasCurrentCostChanged(UpdateCostParameters originalCost,
                                      List<UpdateCostParameters> newCosts)
    {
        if (originalCost == null || newCosts == null)
        {
            return false;
        }

        var newItem = newCosts.FirstOrDefault(x => x.Id == originalCost.Id);
        if (newItem is not null)
        {
            return (newItem.Amount != originalCost.Amount);
        }

        return false;
    }

    private async ValueTask<UpdateCostParameters> ObtainExistingCost()
    {
        var existingCostsProfile = await _paymentRequestRepository.GetCostsProfile();
        if (existingCostsProfile == null)
        {
            return null;
        }

        var currentMonthPayment = existingCostsProfile.FirstOrDefault(x => x.Type == EPaymentRequestCostType.CurrentPayment);
        if (currentMonthPayment != null)
        {
            return new UpdateCostParameters
            {
                Id = currentMonthPayment.Id,
                Date = DateTime.Parse(currentMonthPayment.ItemName),
                Amount = currentMonthPayment.Value
            };
        }

        return null;
    }
}
