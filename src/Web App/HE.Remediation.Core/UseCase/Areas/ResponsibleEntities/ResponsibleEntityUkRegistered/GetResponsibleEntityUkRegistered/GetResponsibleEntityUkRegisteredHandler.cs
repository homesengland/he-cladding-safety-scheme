using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityUkRegistered.GetResponsibleEntityUkRegistered;

public class GetResponsibleEntityUkRegisteredHandler : IRequestHandler<GetResponsibleEntityUkRegisteredRequest, GetResponsibleEntityUkRegisteredResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetResponsibleEntityUkRegisteredHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetResponsibleEntityUkRegisteredResponse> Handle(GetResponsibleEntityUkRegisteredRequest request, CancellationToken cancellationToken)
    {
        var response = await _connection.QuerySingleOrDefaultAsync<GetResponsibleEntityUkRegisteredResponse>("GetResponsibleEntityUkRegistered",
            new
            {
                ApplicationId = _applicationDataProvider.GetApplicationId()
            });

        return response ?? new GetResponsibleEntityUkRegisteredResponse();
    }
}