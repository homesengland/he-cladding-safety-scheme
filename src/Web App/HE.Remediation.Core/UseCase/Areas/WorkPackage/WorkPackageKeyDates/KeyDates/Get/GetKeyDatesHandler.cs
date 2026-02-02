using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.KeyDates.Get;

public class GetKeyDatesHandler : IRequestHandler<GetKeyDatesRequest, GetKeyDatesResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetKeyDatesHandler(IApplicationDataProvider applicationDataProvider,
                                           IBuildingDetailsRepository buildingDetailsRepository,
                                           IApplicationRepository applicationRepository,
                                           IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetKeyDatesResponse> Handle(GetKeyDatesRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var requiredResult = await _workPackageRepository.GetKeyDates();

        var isCladdingBeingRemoved = await _workPackageRepository.IsCladdingBeingRemoved();

        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetKeyDatesResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            StartDateMonth = requiredResult?.StartDate?.Month,
            StartDateYear = requiredResult?.StartDate?.Year,
            UnsafeCladdingRemovalDateMonth = requiredResult?.UnsafeCladdingRemovalDate?.Month,
            UnsafeCladdingRemovalDateYear = requiredResult?.UnsafeCladdingRemovalDate?.Year,
            ExpectedDateForCompletionMonth = requiredResult?.ExpectedDateForCompletion?.Month,
            ExpectedDateForCompletionYear = requiredResult?.ExpectedDateForCompletion?.Year,
            IsCladdingBeingRemoved = isCladdingBeingRemoved,
            IsSubmitted = isSubmitted
        };
    }
}
