using MediatR;
using HE.Remediation.Core.Interface;
using Microsoft.Data.SqlClient;

namespace HE.Remediation.Core.UseCase.Areas.OrganisationManagement.OrganisationDetails
{
    public class OrganisationDetailsHandler : IRequestHandler<OrganisationDetailsRequest, OrganisationDetailsResponse>
    {
        private readonly IDbConnectionWrapper _connection;

        public OrganisationDetailsHandler(IDbConnectionWrapper connection)
        {
            _connection = connection;
        }

        public async Task<OrganisationDetailsResponse> Handle(OrganisationDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _connection.ExecuteAsync("InsertCollaborationOrganisation", request);
                var response = new OrganisationDetailsResponse()
                {
                    Name = request.Name
                };
                return response;
            }
            catch (SqlException ex) when (ex.Message.Contains("UK_CollaborationOrganisation_RegistrationNumber"))
            {
                throw new DuplicateRegistrationNumberException("Registration number already exists");
            }
            
        }
    }

    public class OrganisationDetailsRequest : IRequest<OrganisationDetailsResponse>
    {
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string UserId { get; set; }
    }

    public class OrganisationDetailsResponse
    {
        public string Name { get; set; }
    }

    public class DuplicateRegistrationNumberException(string message) : ApplicationException(message) { }
}
