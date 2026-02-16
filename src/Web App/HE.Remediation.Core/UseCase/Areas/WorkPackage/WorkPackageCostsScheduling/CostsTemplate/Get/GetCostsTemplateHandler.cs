using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CostsTemplate.Get;

public class GetCostsTemplateHandler : IRequestHandler<GetCostsTemplateRequest, GetCostsTemplateResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;

    public GetCostsTemplateHandler(IApplicationDataProvider applicationDataProvider,
                                   IBuildingDetailsRepository buildingDetailsRepository,
                                   IApplicationRepository applicationRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
    }

    public async ValueTask<GetCostsTemplateResponse> Handle(GetCostsTemplateRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        return new GetCostsTemplateResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}
