using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.RequiresSubcontractors.Get;

public class GetRequiresSubcontractorsHandler : IRequestHandler<GetRequiresSubcontractorsRequest, GetRequiresSubcontractorsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetRequiresSubcontractorsHandler(IApplicationDataProvider applicationDataProvider,
                                   IBuildingDetailsRepository buildingDetailsRepository,
                                   IApplicationRepository applicationRepository, 
                                   IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetRequiresSubcontractorsResponse> Handle(GetRequiresSubcontractorsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var requiresSubcontractorsResponse = await _workPackageRepository.GetCostsScheduleRequiresSubcontractors();
        var soughtQuotesResponse = await _workPackageRepository.GetCostsScheduleSoughtQuotes();
        
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetRequiresSubcontractorsResponse
        {
            RequiresSubcontractors = requiresSubcontractorsResponse,
            SoughtQuotes = soughtQuotesResponse,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted
        };
    }
}
