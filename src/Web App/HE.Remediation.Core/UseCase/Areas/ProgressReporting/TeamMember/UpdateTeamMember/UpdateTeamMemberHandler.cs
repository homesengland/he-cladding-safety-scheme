﻿using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.UpdateTeamMember;

public class UpdateTeamMemberHandler : IRequestHandler<UpdateTeamMemberRequest, Guid>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public UpdateTeamMemberHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Guid> Handle(UpdateTeamMemberRequest request, CancellationToken cancellationToken)
    {
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
            HasChasCertification = request.HasChasCertification
        });

        return teamMemberId;
    }
}