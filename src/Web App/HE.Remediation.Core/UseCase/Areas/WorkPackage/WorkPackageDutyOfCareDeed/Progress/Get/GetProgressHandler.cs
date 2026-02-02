using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageDutyOfCareDeed.Progress.Get;

public class GetProgressHandler : IRequestHandler<GetProgressRequest, GetProgressResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetProgressHandler(IApplicationDataProvider applicationDataProvider,
                                    IBuildingDetailsRepository buildingDetailsRepository,
                                    IApplicationRepository applicationRepository,
                                    IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetProgressResponse> Handle(GetProgressRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var dutyOfCareDeedResult = await _workPackageRepository.GetDutyOfCareDeed();
        var status = await _workPackageRepository.GetDutyOfCareDeedStatus();

        return new GetProgressResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            DateSentToGrantCertifyingOfficer = dutyOfCareDeedResult?.DateSentToGrantCertifyingOfficer,
            DateSignedByGrantCertifyingOfficer = dutyOfCareDeedResult?.DateSignedByGrantCertifyingOfficer,
            Status = status ?? Enums.ETaskStatus.NotStarted
        };
    }
}
