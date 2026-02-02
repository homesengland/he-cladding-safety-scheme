using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.ClosingReport;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetConfirmation;

public class SetConfirmationHandler : IRequestHandler<SetConfirmationRequest>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IClosingReportRepository _closingRequestRepository;
    private readonly IStatusTransitionService _statusTransitionService;

    public SetConfirmationHandler(
        IApplicationDataProvider adp, 
        IClosingReportRepository closingRequestRepository, 
        IStatusTransitionService statusTransitionService)
    {
        _adp = adp;
        _closingRequestRepository = closingRequestRepository;
        _statusTransitionService = statusTransitionService;
    }

    public async ValueTask<Unit> Handle(SetConfirmationRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _adp.GetApplicationId();

        await _closingRequestRepository.CreateClosingReport(applicationId);

        var confirmationParams = new ConfirmationParameters
        {
            FinalCostReport = request?.FinalCostReport,
            ExitFraew = request?.ExitFraew,
            CompletionCertificate = request?.CompletionCertificate,
            InformedPracticalCompletion = request?.InformedPracticalCompletion,
        };

        await _closingRequestRepository.UpdateClosingReportConfirmation(applicationId, confirmationParams);
        await _statusTransitionService.TransitionToStatus(EApplicationStatus.ClosingReportInProgress, applicationIds: applicationId);
        
        return Unit.Value;
    }
}
