using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.WorksAlreadyCompleted.GetWorksAlreadyCompleted;

public class GetWorksAlreadyCompletedHandler : IRequestHandler<GetWorksAlreadyCompletedRequest, GetWorksAlreadyCompletedResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetWorksAlreadyCompletedHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetWorksAlreadyCompletedResponse> Handle(GetWorksAlreadyCompletedRequest request, CancellationToken cancellationToken)
    {
        var worksAlreadyCompleted = await _connection.QuerySingleOrDefaultAsync<bool?>("GetBuildingWorksAlreadyCompleted", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId()
        });

        return new GetWorksAlreadyCompletedResponse
        {
            WorksAlreadyCompleted = worksAlreadyCompleted
        };
    }
}