using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyType.SetResponsibleEntityCompanyType;

public class SetResponsibleEntityCompanyTypeHandler : IRequestHandler<SetResponsibleEntityCompanyTypeRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetResponsibleEntityCompanyTypeHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<Unit> Handle(SetResponsibleEntityCompanyTypeRequest request, CancellationToken cancellationToken)
    {       
        await _connection.ExecuteAsync("UpdateResponsibleEntityCompanyType", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            OrganisationTypeId = (int)request.OrganisationType!.Value
        });

        return Unit.Value;
    }
}