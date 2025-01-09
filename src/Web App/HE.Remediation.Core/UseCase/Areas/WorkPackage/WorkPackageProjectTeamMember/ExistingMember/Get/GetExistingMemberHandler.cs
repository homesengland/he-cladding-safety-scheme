using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.ExistingMember.Get;

public class GetExistingMemberHandler : IRequestHandler<GetExistingMemberRequest, GetExistingMemberResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetExistingMemberHandler(IApplicationDataProvider applicationDataProvider,
        IBuildingDetailsRepository buildingDetailsRepository,
        IApplicationRepository applicationRepository,
        IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetExistingMemberResponse> Handle(GetExistingMemberRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var teamMembers = await _workPackageRepository.GetTeamMembers();

        // Only allow appropriate role requests to select existing team member to copy details from
        switch (request.TeamRole)
        {
            case ETeamRole.LeadContractor:
            {
                // Rule: LeadContractor can not copy information of Quantity Surveyor
                teamMembers = teamMembers
                    .Where(tm => tm.Role != ETeamRole.QuantitySurveyor)
                    .ToList();
                break;
            }
            case ETeamRole.QuantitySurveyor:
            {
                // Rule: QuantitySurveyor can not copy information of Lead contractor
                teamMembers = teamMembers
                    .Where(tm => tm.Role != ETeamRole.LeadContractor)
                    .ToList();
                break;
            }
        }
        
        return new GetExistingMemberResponse
        {
            TeamRole = request.TeamRole,
            TeamMembers = teamMembers,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber            
        };
    }
}