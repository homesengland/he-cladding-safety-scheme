using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
public interface IProgressReportingProjectTeamRepository
{
    Task<GetProjectTeamResult> GetProjectTeam(GetProjectTeamParameters parameters);
    Task<GetProjectTeamNoTeamResult> GetProjectTeamNoTeam(GetProjectTeamNoTeamParameters parameters);
    Task SetProjectTeamNoTeam(SetProjectTeamNoTeamParameters parameters);
    Task<List<GetTeamMembersResult>> GetProjectTeamMembers(GetTeamMembersParameters parameters);
    Task UpdateProjectTeamStatus(Guid progressReportId, ETaskStatus status);
    Task<GetProjectTeamTeamMemberResult> GetProjectTeamMember(GetTeamMemberParameters parameters);
    Task<Guid> UpsertProjectTeamMember(UpsertProjectTeamMemberParameters parameters);
    Task DeleteProjectTeamTeamMember(DeleteProjectTeamTeamMemberParameters parameters);
    Task<GetProjectTeamKeyRolesResult> GetProjectTeamKeyRoles(GetProjectTeamKeyRolesParameters parameters);
    Task SetProjectTeamKeyRoles(SetProjectTeamKeyRolesParameters parameters);
    Task<GetProjectTeamKeyRolesDetailsResult> GetProjectTeamKeyRolesDetails(Guid progressReportId);

    // Grant Certifying Officer
    Task<GetGrantCertifyingOfficerResult> GetGrantCertifyingOfficer(Guid progressReportId);
    Task SetHasGrantCertifyingOfficer(Guid progressReportId, bool hasGco);
    Task UpdateGrantCertifyingOfficerResponse(Guid progressReportId, int certifyingOfficerResponse);
    Task UpdateGrantCertifyingOfficerTeamMember(Guid progressReportId, Guid projectTeamMemberId);
    Task UpdateGrantCertifyingOfficerDetails(UpdateGrantCertifyingOfficerDetailsParameters parameters);
    Task UpdateGrantCertifyingOfficerAddress(UpdateGrantCertifyingOfficerAddressParameters parameters);
    Task UpdateGrantCertifyingOfficerSignatory(UpdateGrantCertifyingOfficerSignatoryParameters parameters);
    Task ConfirmGrantCertifyingOfficer(Guid progressReportId);

    Task UpdateGrantCertifyingOfficerDutyOfCareDeedTaskRaised(UpdateGrantCertifyingOfficerDutyOfCareDeedTaskRaisedParameters parameters);

}
