using HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.GetCladdingSystem;

public class GetCladdingSystemHandler : IRequestHandler<GetCladdingSystemRequest, GetCladdingSystemResponse>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAppraisalRepository _fireRiskAppraisalRepository;

    public GetCladdingSystemHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider, IFireRiskAppraisalRepository fireRiskAppraisalRepository)
    {
        _dbConnectionWrapper = dbConnectionWrapper;
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAppraisalRepository = fireRiskAppraisalRepository;
    }

    public async Task<GetCladdingSystemResponse> Handle(GetCladdingSystemRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var response = new GetCladdingSystemResponse();

        if (request.FireRiskCladdingSystemsId.HasValue)
        {
            response = await _dbConnectionWrapper
                .QuerySingleOrDefaultAsync<GetCladdingSystemResponse>("GetCladdingSystem", new { applicationId, request.FireRiskCladdingSystemsId });
        }

        response.CladdingTypes = await _fireRiskAppraisalRepository.GetCladdingSystemTypes();
        response.InsulationTypes = await _fireRiskAppraisalRepository.GetInsulationTypes();

        var manufacturers = await _fireRiskAppraisalRepository.GetActiveCladdingManufacturers();
        response.CladdingManufacturers = manufacturers
            .OrderBy(x => x.IsEndOfList)
            .ThenBy(x => x.Name);

        response.InsulationManufacturers = manufacturers
            .OrderBy(x => x.IsEndOfList)
            .ThenBy(x => x.Name);

        return response;
    }
}