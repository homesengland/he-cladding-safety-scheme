using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentationCompanyOrIndividual.GetRepresentationCompanyOrIndividual;

public class GetRepresentationCompanyOrIndividualHandler : IRequestHandler<GetRepresentationCompanyOrIndividualRequest, GetRepresentationCompanyOrIndividualResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetRepresentationCompanyOrIndividualHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetRepresentationCompanyOrIndividualResponse> Handle(GetRepresentationCompanyOrIndividualRequest request, CancellationToken cancellationToken)
    {
        var responsibleEntityTypeId = await _connection.QuerySingleOrDefaultAsync<int?>("GetRepresentationEntityType", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId()
        });

        return new GetRepresentationCompanyOrIndividualResponse
        {
            ReponsibleEntityType = (EResponsibleEntityType?)responsibleEntityTypeId
        };
    }
}