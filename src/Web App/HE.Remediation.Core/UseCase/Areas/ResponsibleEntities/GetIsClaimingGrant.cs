using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

public class GetIsClaimingGrantHandler : IRequestHandler<GetIsClaimingGrantRequest, GetIsClaimingGrantResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IResponsibleEntityRepository _responsibleEntityRepository;

    public GetIsClaimingGrantHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider, IResponsibleEntityRepository responsibleEntityRepository)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
        _responsibleEntityRepository = responsibleEntityRepository;
    }

    public async ValueTask<GetIsClaimingGrantResponse> Handle(GetIsClaimingGrantRequest request, CancellationToken cancellationToken)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetIsClaimingGrantResponse>("GetIsClaimingGrant", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId()
        });

        var organisationAndRepresentationType = await _responsibleEntityRepository.GetResponsibleEntityOrganisationAndRepresentationType(_applicationDataProvider.GetApplicationId());

        if (result != null)
        {
            result.IsSocialSector = _applicationDataProvider.GetApplicationScheme() == EApplicationScheme.SocialSector;
            result.OrganisationType = organisationAndRepresentationType.OrganisationType;
        }

        return result ?? new GetIsClaimingGrantResponse();
    }
}

public class GetIsClaimingGrantResponse
{
    public bool? IsClaimingGrant { get; set; }
    public bool? HasOwners { get; set; }
    public bool IsSocialSector { get; set; }
    public EApplicationResponsibleEntityOrganisationType OrganisationType { get; set; }
}

public class GetIsClaimingGrantRequest : IRequest<GetIsClaimingGrantResponse>
{
    private GetIsClaimingGrantRequest()
    {
    }

    public static readonly GetIsClaimingGrantRequest Request = new();
}