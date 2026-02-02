using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddExternalWallWorks;

public class GetAddExternalWallWorksHandler: IRequestHandler<GetAddExternalWallWorksRequest, GetAddExternalWallWorksResponse>
{
    private readonly IFireRiskWorksRepository _fireRiskWorksRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;    

    public GetAddExternalWallWorksHandler(IFireRiskWorksRepository fireRiskWorksRepository, 
                                          IApplicationDataProvider applicationDataProvider)
    {
        _fireRiskWorksRepository = fireRiskWorksRepository; 
        _applicationDataProvider = applicationDataProvider;
    }
    public async ValueTask<GetAddExternalWallWorksResponse> Handle(GetAddExternalWallWorksRequest request, CancellationToken cancellationToken)
    {
        if (request?.Id is not null)
        {
            var result = await _fireRiskWorksRepository.GetFireRiskWallWorksDetail(request.Id.Value);
            return new GetAddExternalWallWorksResponse
            {
                Id = request.Id.Value,
                Description = result?.Description
            };           
        }

        return null;
    }

}
