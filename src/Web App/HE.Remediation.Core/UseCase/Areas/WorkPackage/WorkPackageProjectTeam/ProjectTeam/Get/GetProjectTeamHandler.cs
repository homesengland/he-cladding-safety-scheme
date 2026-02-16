using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.ProjectTeam.Get;

public class GetProjectTeamHandler : IRequestHandler<GetProjectTeamRequest, GetProjectTeamResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetProjectTeamHandler(IApplicationDataProvider applicationDataProvider,
                                           IBuildingDetailsRepository buildingDetailsRepository,
                                           IApplicationRepository applicationRepository,
                                           IWorkPackageRepository workPackageRepository)    
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetProjectTeamResponse> Handle(GetProjectTeamRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();
        var isCopy = false;
        
        var teamMembers = await _workPackageRepository.GetTeamMembers();

        if (!teamMembers.Any())
        {
            // Copy team members if available
            var latestTeamProgressReportId = await _workPackageRepository.GetLatestTeamProgressReportId();
            if (latestTeamProgressReportId is not null)
            {
                await _workPackageRepository
                    .CopyLatestTeamFromProgressReport(latestTeamProgressReportId);
            }
            
            teamMembers = await _workPackageRepository.GetTeamMembers();
            isCopy = teamMembers.Any();
        }

        var consumedOptions = teamMembers
            .Select(tm => tm.Role);
        var mandatoryRolesNotYetAdded = Enum.GetValues<ETeamRole>().
            Except(consumedOptions)
            .ToList();
        
        // Other is always optional.
        if (mandatoryRolesNotYetAdded.Contains(ETeamRole.Other))
        {
            mandatoryRolesNotYetAdded.Remove(ETeamRole.Other);
        }
        
        return new GetProjectTeamResponse
        {
            TeamMembers = teamMembers,
            MissingRoles = mandatoryRolesNotYetAdded,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted,
            IsCopy = isCopy,
            HasChasCertificationValue = teamMembers.All(x => x.Role != ETeamRole.LeadContractor)
                ? null
                : teamMembers.Any(x => x.Role == ETeamRole.LeadContractor && x.HasChasCertification.HasValue)
        };
    }
}
