using System.Data;
using Dapper;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Communication.Collaboration;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.InviteMember;

public class SetInviteMemberHandler(IProgressReportingRepository progressReportingRepository, IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider, IBackgroundCollaborationCommunicationQueue emailInviteSendQueue) : IRequestHandler<SetInviteMemberRequest, SetInviteMemberResponse>
{
    private readonly IProgressReportingRepository _progressReportingRepository = progressReportingRepository;
    private readonly IDbConnectionWrapper _connection = connection;
    private readonly IApplicationDataProvider _applicationDataProvider = applicationDataProvider;
    private readonly IBackgroundCollaborationCommunicationQueue _emailInviteSendQueue = emailInviteSendQueue;

    public async Task<SetInviteMemberResponse> Handle(SetInviteMemberRequest request, CancellationToken cancellationToken)
    {
        var teamMember = await _progressReportingRepository.GetTeamMember(request.TeamMemberId);
        var applicationId = _applicationDataProvider.GetApplicationId();

        var collaborationUserId = await InsertThirdPartyCollaborator(teamMember, applicationId);

        // Confirm invite and get details for invite email
        var response = await _connection.QuerySingleOrDefaultAsync<ThirdPartyInviteResponse>(
            "GetCollaborationThirdPartyUserInviteDetails", new { CollaborationUserId = collaborationUserId, request.Auth0UserId, applicationId }
        );

        var collaborationCommunicationRequest = new CollaborationEmailRequest(
            EEmailType.CollaborationThirdPartyInvite,
            teamMember.EmailAddress,
            new()
            {
                    { "FirstName", response.InvitationTo },
                    { "RequestorFullName", response.RequestorFullName },
                    { "BuildingName", response.BuildingName },
                    { "Address", response.Address }
            },
            $"CollaborationOrganisationUsers.Id:{response.CollaborationOrganisationUserId}"
        );

        await _emailInviteSendQueue.QueueAsync(collaborationCommunicationRequest, cancellationToken);

        return new SetInviteMemberResponse
        {
            TeamMemberId = teamMember.TeamMemberId,
            InvitedName = teamMember.Name
        };
    }

    private async Task<Guid?> InsertThirdPartyCollaborator(GetTeamMemberResult teamMember, Guid applicationId)
    {
        var @params = new DynamicParameters();
        @params.Add("@ApplicationId", value: applicationId, dbType: DbType.Guid, direction: ParameterDirection.Input);
        @params.Add("@Email", value: teamMember.EmailAddress, dbType: DbType.String, direction: ParameterDirection.Input);
        @params.Add("@Name", value: teamMember.Name, dbType: DbType.String, direction: ParameterDirection.Input);
        @params.Add("@CollaborationUserId", dbType: DbType.Guid, direction: ParameterDirection.Output);
        await _connection.ExecuteAsync("InsertThirdPartyCollaborator", @params);
        var collaborationUserId = @params.Get<Guid?>("@CollaborationUserId");
        return collaborationUserId;
    }

    public class ThirdPartyInviteResponse
    {
        public string InvitationTo { get; set; }
        public string RequestorFullName { get; set; }
        public string BuildingName { get; set; }
        public string Address { get; set; }
        public string AdminUserEmailAddress { get; set; }
        public Guid? CollaborationOrganisationUserId { get; set; }
    }
}
