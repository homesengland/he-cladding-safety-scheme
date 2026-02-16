using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ExistingTeamMember;

public class GetExistingTeamMemberHandler : IRequestHandler<GetExistingTeamMemberRequest, GetExistingTeamMemberResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetExistingTeamMemberHandler(IApplicationDataProvider applicationDataProvider,
        IBuildingDetailsRepository buildingDetailsRepository,
        IApplicationRepository applicationRepository,
        IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<GetExistingTeamMemberResponse> Handle(GetExistingTeamMemberRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var teamMembers = await _progressReportingRepository.GetTeamMembers();

        // Only allow appropriate role requests to select existing team member to copy details from
        switch (request.TeamRole)
        {
            case ETeamRole.LeadContractor:
            {
                // Rule: LeadContractor can not copy information of Quantity Surveyor
                teamMembers = teamMembers
                    .Where(tm => tm.RoleId != (int)ETeamRole.QuantitySurveyor)
                    .ToList();
                break;
            }
            case ETeamRole.QuantitySurveyor:
            {
                // Rule: QuantitySurveyor can not copy information of Lead contractor
                teamMembers = teamMembers
                    .Where(tm => tm.RoleId != (int)ETeamRole.LeadContractor)
                    .ToList();
                break;
            }
        }
        
        return new GetExistingTeamMemberResponse
        {
            TeamRole = request.TeamRole,
            TeamMembers = teamMembers,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber            
        };
    }
}