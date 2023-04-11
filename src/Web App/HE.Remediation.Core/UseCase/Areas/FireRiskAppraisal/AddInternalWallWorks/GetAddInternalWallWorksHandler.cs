
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddInternalWallWorks;

public class GetAddInternalWallWorksHandler: IRequestHandler<GetAddInternalWallWorksRequest, GetAddInternalWallWorksResponse>
{
    private readonly IFireRiskWorksRepository _fireRiskWorksRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;    

    public GetAddInternalWallWorksHandler(IFireRiskWorksRepository fireRiskWorksRepository, 
                                          IApplicationDataProvider applicationDataProvider)
    {
        _fireRiskWorksRepository = fireRiskWorksRepository; 
        _applicationDataProvider = applicationDataProvider;
    }
    public async Task<GetAddInternalWallWorksResponse> Handle(GetAddInternalWallWorksRequest request, CancellationToken cancellationToken)
    {
        if (request?.Id is not null)
        {
            var result = await _fireRiskWorksRepository.GetFireRiskWallWorksDetail(request.Id.Value);
            return new GetAddInternalWallWorksResponse
            {
                Id = request.Id.Value,
                Description = result?.Description
            };           
        }

        return null;
    }

}