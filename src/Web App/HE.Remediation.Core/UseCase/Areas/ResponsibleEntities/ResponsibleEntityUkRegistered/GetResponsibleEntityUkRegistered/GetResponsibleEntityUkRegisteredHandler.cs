using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityUkRegistered.GetResponsibleEntityUkRegistered;

public class GetResponsibleEntityUkRegisteredHandler : IRequestHandler<GetResponsibleEntityUkRegisteredRequest, GetResponsibleEntityUkRegisteredResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IResponsibleEntityRepository _responsibleEntityRepository;

    public GetResponsibleEntityUkRegisteredHandler(
        IDbConnectionWrapper connection, 
        IApplicationDataProvider applicationDataProvider, 
        IResponsibleEntityRepository responsibleEntityRepository)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
        _responsibleEntityRepository = responsibleEntityRepository;
    }

    public async Task<GetResponsibleEntityUkRegisteredResponse> Handle(GetResponsibleEntityUkRegisteredRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var isUkRegistered = await _connection.QuerySingleOrDefaultAsync<bool?>("GetResponsibleEntityUkRegistered",
            new
            {
                ApplicationId = applicationId
            });
        
        var companyType = await _responsibleEntityRepository.GetResponsibleEntityCompanyType(applicationId);

        return new GetResponsibleEntityUkRegisteredResponse
        {
            UkRegistered = isUkRegistered,
            CompanyType = companyType?.OrganisationType
        };
    }
}