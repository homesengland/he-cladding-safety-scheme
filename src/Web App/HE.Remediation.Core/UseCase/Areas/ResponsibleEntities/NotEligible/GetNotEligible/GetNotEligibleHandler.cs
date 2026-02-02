using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.NotEligible.GetNotEligible;

public class GetNotEligibleHandler : IRequestHandler<GetNotEligibleRequest, GetNotEligibleResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetNotEligibleHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetNotEligibleResponse> Handle(GetNotEligibleRequest request, CancellationToken cancellationToken)
    {
        var response = await _connection.QuerySingleOrDefaultAsync<GetNotEligibleResponse>("GetNotEligibleInformation", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId()
        });

        response ??= new GetNotEligibleResponse();
        response.ApplicationScheme = _applicationDataProvider.GetApplicationScheme();

        return response;
    }
}