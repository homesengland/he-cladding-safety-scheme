using System.Data;
using Dapper;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Communication.Collaboration;
using MediatR;
using static HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.InviteMember.SetInviteMemberHandler;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.RemoveAccess;

public class SetRemoveAccessHandler(IProgressReportingRepository progressReportingRepository, IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider, IBackgroundCollaborationCommunicationQueue emailInviteSendQueue) : IRequestHandler<SetRemoveAccessRequest, SetRemoveAccessResponse>
{
    private readonly IProgressReportingRepository _progressReportingRepository = progressReportingRepository;
    private readonly IDbConnectionWrapper _connection = connection;
    private readonly IApplicationDataProvider _applicationDataProvider = applicationDataProvider;
    private readonly IBackgroundCollaborationCommunicationQueue _emailInviteSendQueue = emailInviteSendQueue;

    public async Task<SetRemoveAccessResponse> Handle(SetRemoveAccessRequest request, CancellationToken cancellationToken)
    {
        var teamMember = await _progressReportingRepository.GetTeamMember(request.TeamMemberId);
        var applicationId = _applicationDataProvider.GetApplicationId();

        var collaborationUserId = await GetCollaborationUserId(teamMember, applicationId);

        // Confirm invite and get details for invite email
        var response = await _connection.QuerySingleOrDefaultAsync<ThirdPartyInviteResponse>(
            "GetCollaborationThirdPartyUserInviteDetails", new { CollaborationUserId = collaborationUserId, request.Auth0UserId, applicationId }
        );

        await RemoveThirdPartyCollaborator(teamMember, applicationId);

        var collaborationCommunicationRequest = new CollaborationEmailRequest(
            EEmailType.CollaborationThirdPartyRemoveAccess,
            teamMember.EmailAddress,
            new()
            {
                    { "FirstName", response.InvitationTo },
                    { "BuildingName", response.BuildingName },
                    { "AdminUserEmailAddress", response.AdminUserEmailAddress }
            },
            $"CollaborationOrganisationUsers.Id:{response.CollaborationOrganisationUserId}"
        );

        await _emailInviteSendQueue.QueueAsync(collaborationCommunicationRequest, cancellationToken);

        return new SetRemoveAccessResponse
        {
            TeamMemberId = teamMember.TeamMemberId,
            InvitedName = teamMember.Name
        };
    }

    private async Task RemoveThirdPartyCollaborator(GetTeamMemberResult teamMember, Guid applicationId)
    {
        await _connection.ExecuteAsync("RemoveThirdPartyCollaborator", new
        {
            teamMember.TeamMemberId,
            ApplicationId = applicationId,
            Email = teamMember.EmailAddress
        });
    }

    private async Task<Guid?> GetCollaborationUserId(GetTeamMemberResult teamMember, Guid applicationId)
    {
        var @params = new DynamicParameters();
        @params.Add("@ApplicationId", value: applicationId, dbType: DbType.Guid, direction: ParameterDirection.Input);
        @params.Add("@Email", value: teamMember.EmailAddress, dbType: DbType.String, direction: ParameterDirection.Input);
        @params.Add("@CollaborationUserId", dbType: DbType.Guid, direction: ParameterDirection.Output);
        await _connection.ExecuteAsync("GetCollaborationUserId", @params);
        var collaborationUserId = @params.Get<Guid?>("@CollaborationUserId");
        return collaborationUserId;
    }
}
