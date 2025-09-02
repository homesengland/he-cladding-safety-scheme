using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepCompanyOrIndividual.GetRepCompanyOrIndividual;

public class GetRepCompanyOrIndividualHandler : IRequestHandler<GetRepCompanyOrIndividualRequest, GetRepCompanyOrIndividualResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetRepCompanyOrIndividualHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetRepCompanyOrIndividualResponse> Handle(GetRepCompanyOrIndividualRequest request, CancellationToken cancellationToken)
    {
        var responsibleEntityTypeId = await _connection.QuerySingleOrDefaultAsync<int?>("GetRepresentationEntityType", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId()
        });

        return new GetRepCompanyOrIndividualResponse
        {
            ReponsibleEntityType = (EResponsibleEntityType?)responsibleEntityTypeId
        };
    }
}