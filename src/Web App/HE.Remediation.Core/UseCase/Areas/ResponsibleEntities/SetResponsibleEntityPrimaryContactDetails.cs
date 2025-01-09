using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

public class SetResponsibleEntityPrimaryContactDetailsHandler : IRequestHandler<SetResponsibleEntityPrimaryContactDetailsRequest, SetResponsibleEntityPrimaryContactDetailsResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IResponsibleEntityRepository _responsibleEntityRepository;

    public SetResponsibleEntityPrimaryContactDetailsHandler(
        IDbConnectionWrapper connection, 
        IApplicationDataProvider applicationDataProvider, 
        IResponsibleEntityRepository responsibleEntityRepository)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
        _responsibleEntityRepository = responsibleEntityRepository;
    }

    public async Task<SetResponsibleEntityPrimaryContactDetailsResponse> Handle(SetResponsibleEntityPrimaryContactDetailsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        await UpdateResponsibleEntityPrimaryContactDetails(applicationId, request);

        var result = await _responsibleEntityRepository.GetResponsibleEntityOrganisationAndRepresentationType(applicationId);

        return new SetResponsibleEntityPrimaryContactDetailsResponse
        {
            OrganisationType = result.OrganisationType,
            RepresentationType = result.RepresentationType
        };
    }

    private async Task UpdateResponsibleEntityPrimaryContactDetails(Guid applicationId, SetResponsibleEntityPrimaryContactDetailsRequest request)
    {
        await _connection.ExecuteAsync("UpdateResponsibleEntityPrimaryContactDetails",
            new
            {
                applicationId,
                request.FirstName,
                request.LastName,
                request.EmailAddress,
                request.ContactNumber
            });
    }
}

public class SetResponsibleEntityPrimaryContactDetailsRequest : IRequest<SetResponsibleEntityPrimaryContactDetailsResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string ContactNumber { get; set; }
}

public class SetResponsibleEntityPrimaryContactDetailsResponse
{
    public EApplicationResponsibleEntityOrganisationType OrganisationType { get; set; }
    public EApplicationRepresentationType RepresentationType { get; set; }
}