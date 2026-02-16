using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageInternalDefects.InternalDefectsCost.Set;
using Mediator;
namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageInternalDefects.InternalDefectsCost.Get;

public class InternalDefectsCostHandler : IRequestHandler<GetInternalDefectsCostRequest, GetInternalDefectsCostResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public InternalDefectsCostHandler(
        IApplicationDataProvider applicationDataProvider,
        IApplicationRepository applicationRepository,
        IBuildingDetailsRepository buildingDetailsRepository,
        IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetInternalDefectsCostResponse> Handle(GetInternalDefectsCostRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        var internalDefects = await _workPackageRepository.GetInternalDefectsCost();
        if (internalDefects is null)
        {
            await _workPackageRepository.InsertInternalDefectsCost();
        }

        var internalDefectsStatus = await _workPackageRepository.GetInternalDefectsStatus();

        if (internalDefectsStatus is null || internalDefectsStatus.Value == ETaskStatus.NotStarted)
        {
            await _workPackageRepository.UpdateInternalDefectsStatus(ETaskStatus.InProgress);
        }

        var interalDefectsCostResponse = await _workPackageRepository.GetInternalDefectsCost();

        return new GetInternalDefectsCostResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            IsSubmitted = isSubmitted,
            InternalDefectsCosts = interalDefectsCostResponse?.InternalDefectsCosts,
            Description = interalDefectsCostResponse?.Description
        };
    }
}

public class GetInternalDefectsCostRequest : IRequest<GetInternalDefectsCostResponse>
{
    private GetInternalDefectsCostRequest()
    {
    }

    public static readonly GetInternalDefectsCostRequest Request = new();
}

public class GetInternalDefectsCostResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
    public decimal? InternalDefectsCosts { get; set; }
    public string Description { get; set; }
}
