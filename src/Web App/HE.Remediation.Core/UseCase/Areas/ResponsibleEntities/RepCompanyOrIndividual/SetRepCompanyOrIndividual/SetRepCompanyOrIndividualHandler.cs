using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepCompanyOrIndividual.SetRepCompanyOrIndividual;

public class SetRepCompanyOrIndividualHandler : IRequestHandler<SetRepCompanyOrIndividualRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetRepCompanyOrIndividualHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<Unit> Handle(SetRepCompanyOrIndividualRequest request, CancellationToken cancellationToken)
    {
        await SaveCompanyOrIndividual(request.ReponsibleEntityType!.Value);
        return Unit.Value;
    }

    private async Task SaveCompanyOrIndividual(EResponsibleEntityType responsibleEntityType)
    {
        await _connection.ExecuteAsync("UpdateRepresentationEntityType", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            ResponsibleEntityTypeId = (int)responsibleEntityType
        });
    }
}