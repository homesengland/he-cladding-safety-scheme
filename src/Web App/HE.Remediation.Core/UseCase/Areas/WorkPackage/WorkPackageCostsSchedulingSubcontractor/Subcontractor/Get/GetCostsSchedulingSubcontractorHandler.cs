using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Subcontractor.Get;

public class GetCostsSchedulingSubcontractorHandler : IRequestHandler<GetCostsSchedulingSubcontractorRequest, GetCostsSchedulingSubcontractorResponse>
{
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetCostsSchedulingSubcontractorHandler(
        IBuildingDetailsRepository buildingDetailsRepository,
        IApplicationRepository applicationRepository,
        IApplicationDataProvider applicationDataProvider,
        IWorkPackageRepository workPackageRepository)
    {
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _applicationDataProvider = applicationDataProvider;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetCostsSchedulingSubcontractorResponse> Handle(GetCostsSchedulingSubcontractorRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var referenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var subcontractor = request.SubcontractorId.HasValue
            ? await _workPackageRepository.GetCostsScheduleSubcontractor(request.SubcontractorId)
            : null;

        if (subcontractor is null && request.SubcontractorId.HasValue)
        {
            throw new EntityNotFoundException("Team Member not found");
        }

        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetCostsSchedulingSubcontractorResponse
        {
            SubcontractorId = request.SubcontractorId.HasValue ? subcontractor?.SubcontractorId : null,
            CompanyName = subcontractor?.CompanyName,
            CompanyRegistration = subcontractor?.CompanyRegistration,
            ApplicationReferenceNumber = referenceNumber,
            BuildingName = buildingName,
            IsSubmitted = isSubmitted
        };
    }
}