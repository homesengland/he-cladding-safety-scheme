using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Communication;
using MediatR;
using System.Transactions;
using HE.Remediation.Core.Services.StatusTransition;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetDeclaration;

public class SetDeclarationHandler : IRequestHandler<SetDeclarationRequest>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IClosingReportRepository _closingRequestRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly ICommunicationService _communicationService;
    private readonly IStatusTransitionService _statusTransitionService;

    public SetDeclarationHandler(
        IApplicationDataProvider adp,
        IClosingReportRepository closingRequestRepository,
        IApplicationRepository applicationRepository,
        ICommunicationService communicationService, 
        IStatusTransitionService statusTransitionService)
    {
        _adp = adp;
        _closingRequestRepository = closingRequestRepository;
        _applicationRepository = applicationRepository;
        _communicationService = communicationService;
        _statusTransitionService = statusTransitionService;
    }

    public async Task<Unit> Handle(SetDeclarationRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _adp.GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _closingRequestRepository.UpdateClosingReportProjectDate(applicationId, request.DateOfCompletion);
        await _closingRequestRepository.UpdateClosingReportFraewRiskToLifeReduced(applicationId, request.FraewRiskToLifeReduced);
        await _closingRequestRepository.UpdateClosingReportToSubmitted(applicationId);

        await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.ClosingReportSubmitted, applicationIds: applicationId);
        await _applicationRepository.UpdateApplicationStage(applicationId, EApplicationStage.WorksCompleted);

        await _communicationService.QueueEmailCommunication(new EmailCommunicationRequest
        (
            applicationId,
            EEmailType.ClosingReportSubmitted
        ));

        scope.Complete();
        
        return Unit.Value;
    }
}
