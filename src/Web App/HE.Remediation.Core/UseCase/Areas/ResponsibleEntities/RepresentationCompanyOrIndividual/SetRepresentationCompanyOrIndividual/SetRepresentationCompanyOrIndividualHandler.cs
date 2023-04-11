using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentationCompanyOrIndividual.SetRepresentationCompanyOrIndividual;

public class SetRepresentationCompanyOrIndividualHandler : IRequestHandler<SetRepresentationCompanyOrIndividualRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetRepresentationCompanyOrIndividualHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<Unit> Handle(SetRepresentationCompanyOrIndividualRequest request, CancellationToken cancellationToken)
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