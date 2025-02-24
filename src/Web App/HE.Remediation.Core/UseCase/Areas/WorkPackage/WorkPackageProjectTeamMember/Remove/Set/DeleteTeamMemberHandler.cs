using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.Remove.Set;

public class DeleteTeamMemberHandler : IRequestHandler<DeleteTeamMemberRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public DeleteTeamMemberHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(DeleteTeamMemberRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        await _workPackageRepository.DeleteTeamMember(request.TeamMemberId);

        var teamMembers = await _workPackageRepository.GetTeamMembers();
        
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
        
        if (mandatoryRolesNotYetAdded.Any())
        {
            await _workPackageRepository.UpdateTeamStatus(ETaskStatus.InProgress);
        }
        
        scope.Complete();
        
        return Unit.Value;
    }
}
