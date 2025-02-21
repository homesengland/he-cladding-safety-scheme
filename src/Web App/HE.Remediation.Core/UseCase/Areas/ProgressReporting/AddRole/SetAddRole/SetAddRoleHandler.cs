
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AddRole.SetAddRole;

public class SetAddRoleHandler : IRequestHandler<SetAddRoleRequest, SetAddRoleResponse>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetAddRoleHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<SetAddRoleResponse> Handle(SetAddRoleRequest request, CancellationToken cancellationToken)
    {
        var teamMembers = await _progressReportingRepository.GetTeamMembers();
        var teamMemberRoles = teamMembers
        .Where(tm => tm.RoleId.HasValue)
        .Select(tm => tm.RoleId.Value);

        // Only allow appropriate role requests to select existing team member to copy details from
        bool canChooseExistingMembers;
        switch (request.TeamRole)
        {
            case ETeamRole.LeadContractor:
                {
                    // Rule: LeadContractor can not copy information of Quantity Surveyor
                    canChooseExistingMembers = teamMemberRoles.Except(new[] { (int)ETeamRole.QuantitySurveyor }).Any();
                    break;
                }
            case ETeamRole.QuantitySurveyor:
                {
                    // Rule: QuantitySurveyor can not copy information of Lead contractor
                    canChooseExistingMembers = teamMemberRoles.Except(new[] { (int)ETeamRole.LeadContractor }).Any();
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
