
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.RecommendedWorksStart.GetRecommendedWorksStart;

public class GetRecommendedWorksStartHandler : IRequestHandler<GetRecommendedWorksStartRequest, GetRecommendedWorksStartResponse>
{
    private readonly IFireRiskWorksRepository _fireRiskWorksRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetRecommendedWorksStartHandler(IFireRiskWorksRepository fireRiskWorksRepository, IApplicationDataProvider applicationDataProvider)
    {
        _fireRiskWorksRepository = fireRiskWorksRepository; 
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetRecommendedWorksStartResponse> Handle(GetRecommendedWorksStartRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var result = await _fireRiskWorksRepository.GetWorksRequired(applicationId);

        return new GetRecommendedWorksStartResponse
        {
           InternalWorksRequired = result.InternalWorksRequired
        };
    }
}
