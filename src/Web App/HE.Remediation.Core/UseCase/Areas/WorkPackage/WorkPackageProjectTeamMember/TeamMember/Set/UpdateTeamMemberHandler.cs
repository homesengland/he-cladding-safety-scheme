using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.ProjectTeam;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.TeamMember.Set;

public class UpdateTeamMemberHandler : IRequestHandler<UpdateTeamMemberRequest, Guid>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public UpdateTeamMemberHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Guid> Handle(UpdateTeamMemberRequest request, CancellationToken cancellationToken)
    {
        var considerateConstructorSchemeReason = request.ConsiderateConstructorSchemeType switch
        {
            EConsiderateConstructorSchemeType.No => request.ConsiderateConstructorSchemeReasonNo,
            EConsiderateConstructorSchemeType.DontKnow => request.ConsiderateConstructorSchemeReasonDontKnow,
            _ => null
        };

        var teamMemberId = await _workPackageRepository.UpsertTeamMember(new UpsertTeamMemberParameters
        {
            CompanyName = request.CompanyName,
            CompanyRegistration = request.CompanyRegistration,
            ConsiderateConstructorSchemeTypeId = (int?)request.ConsiderateConstructorSchemeType,
            ContractSigned = request.ContractSigned,
            EmailAddress = request.EmailAddress,
            IndemnityInsurance = request.IndemnityInsurance,
            IndemnityInsuranceReason = request.IndemnityInsuranceReason,
            InvolvedInOriginalInstallation = request.InvolvedInOriginalInstallation,
            InvolvedRoleReason = request.InvolvedRoleReason,
            Name = request.Name,
            OtherRole = request.OtherRole,
            PrimaryContactNumber = request.PrimaryContactNumber,
            TeamMemberId = request.TeamMemberId,
            TeamRoleId = (int)request.Role,
            HasChasCertification = request.HasChasCertification,
            ConsiderateConstructorSchemeReason = considerateConstructorSchemeReason,
            CreatedDate = DateTime.UtcNow,
            LastModifiedDate = DateTime.UtcNow
        });

        return teamMemberId;
    }
}