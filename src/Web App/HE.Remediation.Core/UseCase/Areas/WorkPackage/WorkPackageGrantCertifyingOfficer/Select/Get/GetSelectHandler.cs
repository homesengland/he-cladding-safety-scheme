using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Select.Get;

public class GetSelectHandler : IRequestHandler<GetSelectRequest, GetSelectResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetSelectHandler(IApplicationDataProvider applicationDataProvider,
                            IBuildingDetailsRepository buildingDetailsRepository,
                            IApplicationRepository applicationRepository,
                            IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetSelectResponse> Handle(GetSelectRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var candidates = await _workPackageRepository.GetGrantCertifyingOfficerCandidates();
        var selectedProjectTeamMemberId = await _workPackageRepository.GetGrantCertifyingOfficerProjectTeamMemberId();

        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetSelectResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            Candidates = candidates,
            SelectedProjectTeamMemberId = selectedProjectTeamMemberId,
            IsSubmitted = isSubmitted
        };
    }
}
