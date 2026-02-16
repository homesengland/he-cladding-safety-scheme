using Mediator;
using HE.Remediation.Core.Interface;
using Dapper;
using System.Data;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.OrganisationManagement.Users
{
    public class UsersHandler : IRequestHandler<UsersRequest, UsersResponse>
    {
        private readonly IDbConnectionWrapper _connection;

        public UsersHandler(IDbConnectionWrapper connection)
        {
            _connection = connection;
        }

        public async ValueTask<UsersResponse> Handle(UsersRequest request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Auth0UserId", request.Auth0UserId);
            parameters.Add("@OrganisationJson", dbType: DbType.String, size: -1, direction: ParameterDirection.Output);
            parameters.Add("@ApplicationRoleId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                var users = await _connection.QueryAsync<UsersResponse.CollaborationUser>("GetCollaborationOrganisationUsers", parameters);

                var orgMetadataJson = parameters.Get<string>("OrganisationJson");
                UsersResponse.CollaborationOrganisation orgMetadata = null;
                if (!string.IsNullOrEmpty(orgMetadataJson))
                {
                    orgMetadata = JsonSerializer.Deserialize<UsersResponse.CollaborationOrganisation>(orgMetadataJson);
                }

                var applicationRoleId = parameters.Get<int?>("ApplicationRoleId");

                var response = new UsersResponse()
                {
                    Users = [.. users],
                    Organisation = orgMetadata,
                    ApplicationRoleId = (EApplicationRole)(applicationRoleId ?? (int)EApplicationRole.OrganisationUser)
                };
                return response;
            }
            catch (SqlException ex) when (ex.Message.Contains("No organisation found"))
            {
                throw new OrganisationNotExistsException();
            }

        }
    }

    public class UsersRequest : IRequest<UsersResponse>
    {
        public UsersRequest(string auth0UserId)
        {
            Auth0UserId = auth0UserId;
        }

        public string Auth0UserId { get; private set; }
    }

    public class UsersResponse
    {
        public CollaborationOrganisation Organisation { get; set; }
        public List<CollaborationUser> Users { get; set; }
        public EApplicationRole ApplicationRoleId { get; set; }

        public record CollaborationUser(Guid Id, string Name, string Email, int ApplicationRoleId, int UserStatusId, string Auth0UserId);
        public record CollaborationOrganisation(Guid Id, string Name);
    }

    public class OrganisationNotExistsException() : ApplicationException() { };
    
}
