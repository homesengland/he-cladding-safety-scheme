using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.PaymentRequest;
using HE.Remediation.Core.Interface;
using MediatR;
namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetThirdPartyContributionsChanged;

public class SetThirdPartyContributionsChangedHandler : IRequestHandler<SetThirdPartyContributionsChangedRequest, SetThirdPartyContributionsChangedResponse>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public SetThirdPartyContributionsChangedHandler(IApplicationDataProvider adp,
                                  IPaymentRequestRepository paymentRequestRepository)
    {
        _adp = adp;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async Task<SetThirdPartyContributionsChangedResponse> Handle(SetThirdPartyContributionsChangedRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _adp.GetApplicationId(); 
        var paymentRequestId = _adp.GetPaymentRequestId();

        PaymentRequestThirdPartyContributionsChangedParameters thirdPartyContributionsParams = new PaymentRequestThirdPartyContributionsChangedParameters
        {
            PaymentRequestId = paymentRequestId,
            ThirdPartyContributionsChanged = request?.ThirdPartyContributionsChanged
        };

        await _paymentRequestRepository.UpdatePaymentRequestThirdPartyContributionsChangedDetails( thirdPartyContributionsParams);

        var projectDetails = await _paymentRequestRepository.GetPaymentRequestProjectDetails(paymentRequestId);

        var noOfMonthsSlippage = 2;
        var endDateSlipped = projectDetails.ProjectDatesChanged == true && await EndDateHasSlipped(applicationId, noOfMonthsSlippage);

        return new SetThirdPartyContributionsChangedResponse
        {
            ThirdPartyContributionsChanged = request?.ThirdPartyContributionsChanged,
            CostsChanged = projectDetails?.CostsChanged,
            UnsafeCladdingRemoved = projectDetails?.UnsafeCladdingRemoved,
            EndDateSlipped = endDateSlipped
        };
    }


    public async Task<bool> EndDateHasSlipped(Guid applicationId, int slippageInMonths)
    {
        var endVersionDates = await _paymentRequestRepository.GetPaymentRequestEndVersionDates(applicationId);
        if (endVersionDates == null)
        {
            return false;
        }
        if (endVersionDates.MostRecentVersion > 1)
        {
            if ((endVersionDates.MostRecentEndDate != null) &&
                (endVersionDates.PreviousEndDate != null))
            {
                var monthDifference = ((endVersionDates.MostRecentEndDate.Value.Month + endVersionDates.MostRecentEndDate.Value.Year * 12) -
                                       (endVersionDates.PreviousEndDate.Value.Month + endVersionDates.PreviousEndDate.Value.Year * 12));
                return (Math.Abs(monthDifference) >= slippageInMonths);
            }
        }

        return false;
    }
}
