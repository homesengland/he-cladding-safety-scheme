using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.AddRole.Set;

public class SetAddRoleHandler : IRequestHandler<SetAddRoleRequest, SetAddRoleResponse>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetAddRoleHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<SetAddRoleResponse> Handle(SetAddRoleRequest request, CancellationToken cancellationToken)
    {
        var teamMembers = await _workPackageRepository.GetTeamMembers();
        var teamMemberRoles = teamMembers.Select(tm => tm.Role);

        // Only allow appropriate role requests to select existing team member to copy details from
        bool canChooseExistingMembers;
        switch (request.TeamRole)
        {
            case ETeamRole.LeadContractor:
            {
                // Rule: LeadContractor can not copy information of Quantity Surveyor
                canChooseExistingMembers = teamMemberRoles.Except(new[] { ETeamRole.QuantitySurveyor }).Any();
                break;
            }
            case ETeamRole.QuantitySurveyor:
            {
                // Rule: QuantitySurveyor can not copy information of Lead contractor
                canChooseExistingMembers = teamMemberRoles.Except(new[] { ETeamRole.LeadContractor }).Any();
                break;
            }
            default:
                canChooseExistingMembers = teamMembers.Any();
                break;
        }

        return new SetAddRoleResponse
        {
            CanChooseExistingMembers = canChooseExistingMembers
        };
    }
}