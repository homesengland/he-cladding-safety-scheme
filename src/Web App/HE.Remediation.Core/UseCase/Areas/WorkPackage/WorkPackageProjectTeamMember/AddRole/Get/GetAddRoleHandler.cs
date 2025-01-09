using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.AddRole.Get;

public class GetAddRoleHandler : IRequestHandler<GetAddRoleRequest, GetAddRoleResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetAddRoleHandler(IApplicationDataProvider applicationDataProvider,
                                           IBuildingDetailsRepository buildingDetailsRepository,
                                           IApplicationRepository applicationRepository,
                                           IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetAddRoleResponse> Handle(GetAddRoleRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var teamMembers = await _workPackageRepository.GetTeamMembers();

        var allOptions = Enum.GetValues<ETeamRole>().ToList();
        var consumedOptions = teamMembers.Select(tm => (ETeamRole)tm.Role);
        var availableOptions = allOptions.Except(consumedOptions).ToList();

        // Other should always be available.
        if (!availableOptions.Contains(ETeamRole.Other))
        {
            availableOptions.Add(ETeamRole.Other);
        }

        return new GetAddRoleResponse
        {
            AvailableTeamRoles = availableOptions,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber            
        };
    }
}