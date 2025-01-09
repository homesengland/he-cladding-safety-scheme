using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.NoQuotes.Get;

public class GetNoQuotesHandler : IRequestHandler<GetNoQuotesRequest, GetNoQuotesResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetNoQuotesHandler(IApplicationDataProvider applicationDataProvider,
                                   IBuildingDetailsRepository buildingDetailsRepository,
                                   IApplicationRepository applicationRepository, 
                                   IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetNoQuotesResponse> Handle(GetNoQuotesRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var response = await _workPackageRepository.GetCostsScheduleNoQuotesReason();

        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetNoQuotesResponse
        {
            Reason = response,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted
        };
    }
}
