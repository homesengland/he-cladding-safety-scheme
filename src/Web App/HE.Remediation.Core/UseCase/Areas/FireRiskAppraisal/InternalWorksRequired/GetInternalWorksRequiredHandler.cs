
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InternalWorksRequired;

public class GetInternalWorksRequiredHandler: IRequestHandler<GetInternalWorksRequiredRequest, GetInternalWorksRequiredResponse>
{
    private readonly IFireRiskWorksRepository _fireRiskWorksRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetInternalWorksRequiredHandler(IFireRiskWorksRepository fireRiskWorksRepository, IApplicationDataProvider applicationDataProvider)
    {
        _fireRiskWorksRepository = fireRiskWorksRepository; 
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetInternalWorksRequiredResponse> Handle(GetInternalWorksRequiredRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var result = await _fireRiskWorksRepository.GetWorksRequired(applicationId);

        return new GetInternalWorksRequiredResponse
        {
            WorksRequired = result.InternalWorksRequired,
            ExternalWorksRequired = result.ExternalWorksRequired
        };
    }
}
