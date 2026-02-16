using Mediator;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.SetReasonForClosing
{
    public class SetReasonForClosingHandler : IRequestHandler<SetReasonForClosingRequest>
    {
        private readonly IApplicationDataProvider _adp;
        private readonly IApplicationRepository _applicationRepository;

        public SetReasonForClosingHandler(IApplicationDataProvider adp,
                                      IApplicationRepository applicationRepository)
        {
            _adp = adp;
            _applicationRepository = applicationRepository;
        }

        public async ValueTask<Unit> Handle(SetReasonForClosingRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _adp.GetApplicationId();

            await _applicationRepository.UpdateApplicationReasonForWithdrawalRequest(applicationId, request.ReasonForClosing);
            return Unit.Value;
        }
    }
}
