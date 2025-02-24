using HE.Remediation.Core.Data.Repositories;
using MediatR;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.ProjectTeam;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.UpdateTeamMember;

public class UpdateTeamMemberHandler : IRequestHandler<UpdateTeamMemberRequest, Guid>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public UpdateTeamMemberHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Guid> Handle(UpdateTeamMemberRequest request, CancellationToken cancellationToken)
    {
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
            HasChasCertification = request.HasChasCertification
        });

        return teamMemberId;
    }
}
