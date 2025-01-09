using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

public class SetIsClaimingGrantHandler : IRequestHandler<SetIsClaimingGrantRequest, SetIsClaimingGrantResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IResponsibleEntityRepository _responsibleEntityRepository;
    private readonly IApplicationRepository _applicationRepository;

    public SetIsClaimingGrantHandler(
        IDbConnectionWrapper connection, 
        IApplicationDataProvider applicationDataProvider, 
        IResponsibleEntityRepository responsibleEntityRepository,
        IApplicationRepository applicationRepository)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
        _responsibleEntityRepository = responsibleEntityRepository;
        _applicationRepository = applicationRepository;
    }

    public async Task<SetIsClaimingGrantResponse> Handle(SetIsClaimingGrantRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        await _connection.ExecuteAsync("UpdateIsClaimingGrant", new
        {
            ApplicationId = applicationId,
            request.IsClaimingGrant
        });

        await _applicationRepository.UpdateStatus(applicationId, EApplicationStatus.ApplicationInProgress);

        var result = await _responsibleEntityRepository.GetResponsibleEntityCompanyType(applicationId);
        return new SetIsClaimingGrantResponse
        {
            CompanyType = result.OrganisationType!.Value
        };
    }
}

public class SetIsClaimingGrantResponse
{
    public EApplicationResponsibleEntityOrganisationType CompanyType { get; set; }
}

public class SetIsClaimingGrantRequest : IRequest<SetIsClaimingGrantResponse>
{
    public bool? IsClaimingGrant { get; set; }
    public bool? HasOwners { get; set; }
}