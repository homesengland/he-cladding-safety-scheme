using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetReviewPayment;

public class SetReviewPaymentHandler : IRequestHandler<SetReviewPaymentRequest>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IClosingReportRepository _closingReportRequestRepository;

    public SetReviewPaymentHandler(IApplicationDataProvider adp, 
                                  IClosingReportRepository closingReportRepository)
    {
        _adp = adp;
        _closingReportRequestRepository = closingReportRepository;
    }

    public async ValueTask<Unit> Handle(SetReviewPaymentRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _adp.GetApplicationId();

        if (request.ChangeToMonthlyCost)
        {
            await _closingReportRequestRepository.UpdateClosingReportReasonForChange(applicationId, request.ReasonForChange);
        }

        if(request.Confirm)
        {
            await _closingReportRequestRepository.UpsertClosingReportTaskStatus(applicationId, EClosingReportTask.SubmitPaymentRequest, ETaskStatus.Completed);
        }
        
        return Unit.Value;
    } 
}
