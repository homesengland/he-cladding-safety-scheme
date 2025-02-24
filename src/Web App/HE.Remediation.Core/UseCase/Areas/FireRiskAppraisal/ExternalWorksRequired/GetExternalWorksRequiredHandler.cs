using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWorksRequired
{
    public class GetExternalWorksRequiredHandler : IRequestHandler<GetExternalWorksRequiredRequest, GetExternalWorksRequiredResponse>
    {
        private readonly IFireRiskWorksRepository _fireRiskWorksRepository;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetExternalWorksRequiredHandler(IFireRiskWorksRepository fireRiskWorksRepository, IApplicationDataProvider applicationDataProvider)
        {
            _fireRiskWorksRepository = fireRiskWorksRepository; 
            _applicationDataProvider = applicationDataProvider;
        }
        public async Task<GetExternalWorksRequiredResponse> Handle(GetExternalWorksRequiredRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var result = await _fireRiskWorksRepository.GetWorksRequired(applicationId);

            return new GetExternalWorksRequiredResponse
            {
                WorksRequired = result.ExternalWorksRequired
            };
        }
    }
}
