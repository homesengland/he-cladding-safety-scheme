using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.UpdateTeamMember;

public class UpdateTeamMemberHandler : IRequestHandler<UpdateTeamMemberRequest, Guid>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public UpdateTeamMemberHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Guid> Handle(UpdateTeamMemberRequest request, CancellationToken cancellationToken)
    {
        var considerateConstructorSchemeReason = request.ConsiderateConstructorSchemeType switch
        {
            EConsiderateConstructorSchemeType.No => request.ConsiderateConstructorSchemeReasonNo,
            EConsiderateConstructorSchemeType.DontKnow => request.ConsiderateConstructorSchemeReasonDontKnow,
            _ => null
        };

        var teamMemberId = await _progressReportingRepository.UpsertTeamMember(new UpsertTeamMemberParameters
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