using Dapper;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
public class ProgressReportingProjectTeamRepository : IProgressReportingProjectTeamRepository
{
    private readonly IDbConnectionWrapper _connection;

    public ProgressReportingProjectTeamRepository(
        IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<GetProjectTeamResult> GetProjectTeam(GetProjectTeamParameters parameters)
    {
        GetProjectTeamResult result = null;

        await _connection
            .QueryAsync<GetProjectTeamResult, GetProjectTeamResult.ProjectTeamMemberResult, GetProjectTeamResult>(
                nameof(GetProjectTeam),
                (projectTeam, member) =>
                {
                    result ??= projectTeam;

                    if (member is not null && result.TeamMembers.All(x => x.Id != member.Id))
                    {
                        result.TeamMembers.Add(member);
                    }

                    return result;
                },
                parameters);

        return result ?? new GetProjectTeamResult();
    }

    public async Task<GetProjectTeamNoTeamResult> GetProjectTeamNoTeam(GetProjectTeamNoTeamParameters parameters)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetProjectTeamNoTeamResult>("GetProjectTeamNoTeam", parameters);
        return result;
    }

    public async Task<List<GetTeamMembersResult>> GetProjectTeamMembers(GetTeamMembersParameters parameters)
    {
        var result = await _connection.QueryAsync<GetTeamMembersResult>("GetProjectTeamMembers", parameters);
        return result.ToList();
    }

    public async Task SetProjectTeamNoTeam(SetProjectTeamNoTeamParameters parameters)
    {
        await _connection.ExecuteAsync("SetProjectTeamNoTeam", parameters);
    }

    public async Task UpdateProjectTeamStatus(Guid progressReportId, ETaskStatus status)
    {
        await _connection.ExecuteAsync("UpdateProjectTeamStatus", new { ProgressReportId = progressReportId, TaskStatusId = (int)status });
    }
    
    public async Task<GetProjectTeamTeamMemberResult> GetProjectTeamMember(GetTeamMemberParameters parameters)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetProjectTeamTeamMemberResult>("GetProjectTeamMember", parameters);
        return result;
    }

    public async Task<Guid> UpsertProjectTeamMember(UpsertProjectTeamMemberParameters parameters)
    {
        return await _connection.QuerySingleOrDefaultAsync<Guid>(nameof(UpsertProjectTeamMember), parameters);
    }
    
    public async Task<GetProjectTeamKeyRolesResult> GetProjectTeamKeyRoles(GetProjectTeamKeyRolesParameters parameters)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetProjectTeamKeyRolesResult>(nameof(GetProjectTeamKeyRoles), parameters);
        return result;
    }

    public async Task SetProjectTeamKeyRoles(SetProjectTeamKeyRolesParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetProjectTeamKeyRoles), parameters);
    }

    public async Task<GetProjectTeamKeyRolesDetailsResult> GetProjectTeamKeyRolesDetails(Guid progressReportId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ProgressReportId", progressReportId);

        var result = new GetProjectTeamKeyRolesDetailsResult();

        await _connection.QueryAsync<
            GetProjectTeamKeyRolesDetailsResult.TeamMemberResult,
            GetProjectTeamKeyRolesDetailsResult.TeamMemberResult,
            GetProjectTeamKeyRolesDetailsResult.TeamMemberResult,
            GetProjectTeamKeyRolesDetailsResult> (
                nameof(GetProjectTeamKeyRolesDetails),
                (applicationLead, leaseholderCommunicator, regulatoryComplianceMember) =>
                {
                    result.ApplicationLead = applicationLead?.Id is not null ? applicationLead : null;
                    result.LeaseholderCommunicator = leaseholderCommunicator;
                    result.RegulatoryComplianceMember = regulatoryComplianceMember;
                    return result;
                },
                parameters);

        return result;
    }

    // Grant Certifying Officer
    public async Task<GetGrantCertifyingOfficerResult> GetGrantCertifyingOfficer(Guid progressReportId)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetGrantCertifyingOfficerResult>("GetGrantCertifyingOfficer", 
            new { ProgressReportId = progressReportId });
        return result;
    }

    public async Task SetHasGrantCertifyingOfficer(Guid progressReportId, bool hasGco)
    {
        await _connection.ExecuteAsync("SetHasGrantCertifyingOfficer", 
            new { ProgressReportId = progressReportId, HasGco = hasGco });
    }

    public async Task UpdateGrantCertifyingOfficerTeamMember(Guid progressReportId, Guid projectTeamMemberId)
    {
        await _connection.ExecuteAsync("UpdateGrantCertifyingOfficerTeamMemberV2",
            new { ProgressReportId = progressReportId, ProjectTeamMemberId = projectTeamMemberId });
    }

    public async Task UpdateGrantCertifyingOfficerResponse(Guid progressReportId, int certifyingOfficerResponse)
    {
        await _connection.ExecuteAsync("UpdateGrantCertifyingOfficerResponseV2",
            new { ProgressReportId = progressReportId, CertifyingOfficerResponseId = certifyingOfficerResponse });
    }

    public async Task UpdateGrantCertifyingOfficerDetails(UpdateGrantCertifyingOfficerDetailsParameters parameters)
    {
        await _connection.ExecuteAsync("UpdateGrantCertifyingOfficerDetailsV2", parameters);
    }

    public async Task UpdateGrantCertifyingOfficerAddress(UpdateGrantCertifyingOfficerAddressParameters parameters)
    {
        await _connection.ExecuteAsync("UpdateGrantCertifyingOfficerAddressV2", parameters);
    }

    public async Task UpdateGrantCertifyingOfficerSignatory(UpdateGrantCertifyingOfficerSignatoryParameters parameters)
    {
        await _connection.ExecuteAsync("UpdateGrantCertifyingOfficerSignatoryV2", parameters);
    }

    public async Task ConfirmGrantCertifyingOfficer(Guid progressReportId)
    {
        await _connection.ExecuteAsync("ConfirmGrantCertifyingOfficer", new { ProgressReportId = progressReportId });
    }

    public async Task UpdateGrantCertifyingOfficerDutyOfCareDeedTaskRaised(UpdateGrantCertifyingOfficerDutyOfCareDeedTaskRaisedParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(UpdateGrantCertifyingOfficerDutyOfCareDeedTaskRaised), parameters);
    }

    public async Task DeleteProjectTeamTeamMember(DeleteProjectTeamTeamMemberParameters parameters)
    {
        await _connection.ExecuteAsync("DeleteProjectTeamTeamMember", parameters);
    }
}
