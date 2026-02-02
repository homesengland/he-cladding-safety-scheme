using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWorksRequired
{
    public class SetExternalWorksRequiredHandler : IRequestHandler<SetExternalWorksRequiredRequest>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IFireRiskWorksRepository _fireRiskWorksRepository;

        public SetExternalWorksRequiredHandler(IApplicationDataProvider applicationDataProvider, IFireRiskWorksRepository fireRiskWorksRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _fireRiskWorksRepository = fireRiskWorksRepository;
        }

        public async ValueTask<Unit> Handle(SetExternalWorksRequiredRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _fireRiskWorksRepository.SetExternalWorksRequired(applicationId, request.WorkRequired);

            if (request.WorkRequired == ENoYes.No)
            {
                await _fireRiskWorksRepository.ResetFireRiskWallWorks(applicationId, EWorkType.External);
            }

            return Unit.Value;
        }
    }
}
