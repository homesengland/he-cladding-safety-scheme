using Dapper;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Data.StoredProcedureResults;
using System.Data;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Get;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Invite;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.Repositories;

public interface IThirdPartyCollaboratorRepository
{
    Task<GetThirdPartyResponse> GetTeamMembersForThirdPartyCollaboration(Guid applicationId);
    Task<GetInviteResponse> GetTeamMemberForThirdPartyCollaboration(Guid teamMemberId, ETeamMemberSource source);
}

public class ThirdPartyCollaboratorRepository : IThirdPartyCollaboratorRepository
{
    private readonly IDbConnectionWrapper _connection;

    public ThirdPartyCollaboratorRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<GetThirdPartyResponse> GetTeamMembersForThirdPartyCollaboration(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);
        parameters.Add("@ApplicationReferenceNumber", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);
        parameters.Add("@BuildingName", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

        var teamMembers = (await _connection.QueryAsync<GetThirdPartyResponse.TeamMember>("GetTeamMembersForThirdPartyCollaboration", parameters));

        var applicationReferenceNumber = parameters.Get<string>("ApplicationReferenceNumber");
        var buildingName = parameters.Get<string>("BuildingName");

        var response = new GetThirdPartyResponse()
        {
            TeamMembers = (teamMembers ?? new List<GetThirdPartyResponse.TeamMember>()).ToList(),
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
        return response;
    }

    public async Task<GetInviteResponse> GetTeamMemberForThirdPartyCollaboration(Guid teamMemberId, ETeamMemberSource source)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@TeamMemberId", teamMemberId);
        parameters.Add("@Source", source);

        return (await _connection.QuerySingleOrDefaultAsync<GetInviteResponse>("GetTeamMemberForThirdPartyCollaboration", parameters));
    }
}