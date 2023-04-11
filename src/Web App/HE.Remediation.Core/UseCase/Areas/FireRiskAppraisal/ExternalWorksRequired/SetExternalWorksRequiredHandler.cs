using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

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

        public async Task<Unit> Handle(SetExternalWorksRequiredRequest request, CancellationToken cancellationToken)
        {
            var applicaitonId = _applicationDataProvider.GetApplicationId();

            await _fireRiskWorksRepository.SetExternalWorksRequired(applicaitonId, request.WorkRequired);

            return Unit.Value;
        }
    }
}
