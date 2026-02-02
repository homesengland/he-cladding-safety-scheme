using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanySubType.SetResponsibleEntityCompanySubType;

public class SetResponsibleEntityCompanySubTypeHandler : IRequestHandler<SetResponsibleEntityCompanySubTypeRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetResponsibleEntityCompanySubTypeHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<Unit> Handle(SetResponsibleEntityCompanySubTypeRequest request, CancellationToken cancellationToken)
    {                
        await _connection.ExecuteAsync("UpdateResponsibleEntityCompanySubType", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),            
            OrganisationSubTypeId = request.OrganisationSubType,
            request.OrganisationSubTypeDescription
        });

        return Unit.Value;
    }
}
