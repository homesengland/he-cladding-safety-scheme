using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetNeedVariations
{
    public class SetNeedVariationsHandler : IRequestHandler<SetNeedVariationsRequest>
    {
        private readonly IApplicationDataProvider _adp;
        private readonly IClosingReportRepository _closingRequestRepository;
        private readonly IStatusTransitionService _statusTransitionService;

        public SetNeedVariationsHandler(
            IApplicationDataProvider adp,
            IClosingReportRepository closingRequestRepository,
            IStatusTransitionService statusTransitionService)
        {
            _adp = adp;
            _closingRequestRepository = closingRequestRepository;
            _statusTransitionService = statusTransitionService;
        }

        public async ValueTask<Unit> Handle(SetNeedVariationsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _adp.GetApplicationId();

            await _closingRequestRepository.CreateClosingReport(applicationId);

            await _closingRequestRepository.UpdateClosingReportNeedVariations(applicationId, request?.NeedVariations);
            await _statusTransitionService.TransitionToStatus(EApplicationStatus.ClosingReportInProgress, applicationIds: applicationId);

            return Unit.Value;
        }
    }
}

