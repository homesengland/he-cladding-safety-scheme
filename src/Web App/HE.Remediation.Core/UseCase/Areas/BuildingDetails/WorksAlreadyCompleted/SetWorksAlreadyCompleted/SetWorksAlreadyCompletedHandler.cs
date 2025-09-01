using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.WorksAlreadyCompleted.SetWorksAlreadyCompleted;

public class SetWorksAlreadyCompletedHandler : IRequestHandler<SetWorksAlreadyCompletedRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetWorksAlreadyCompletedHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<Unit> Handle(SetWorksAlreadyCompletedRequest request, CancellationToken cancellationToken)
    {
        await SaveWorksAlreadyCompleted(request);
        return Unit.Value;
    }

    private async Task SaveWorksAlreadyCompleted(SetWorksAlreadyCompletedRequest request)
    {
        await _connection.ExecuteAsync("UpdateBuildingWorksAlreadyCompleted", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            request.WorksAlreadyCompleted
        });
    }
}