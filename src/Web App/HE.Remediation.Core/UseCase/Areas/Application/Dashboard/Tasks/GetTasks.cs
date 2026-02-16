using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Alert;
using HE.Remediation.Core.Services.Alert.Models;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Application.Dashboard.Tasks;

public class GetTasksHandler : IRequestHandler<GetTasksRequest, GetTasksResponse>
{
    private readonly IAlertRepository _alertRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IAlertService _alertService;

    public GetTasksHandler(IAlertRepository alertRepository, IApplicationDataProvider applicationDataProvider, IAlertService alertService)
    {
        _alertRepository = alertRepository;
        _applicationDataProvider = applicationDataProvider;
        _alertService = alertService;
    }

    public async ValueTask<GetTasksResponse> Handle(GetTasksRequest request, CancellationToken cancellationToken)
    {
        var alerts = await _alertRepository.GetAlerts(new GetAlertsParameters
        {
            UserId = _applicationDataProvider.GetUserId()
        });

        var alertModels = await _alertService.CreateAlertModels(alerts);

        return new GetTasksResponse
        {
            Alerts = alertModels.ToList()
        };
    }
}

public class GetTasksRequest: IRequest<GetTasksResponse>
{
    private GetTasksRequest()
    {
    }

    public static readonly GetTasksRequest Request = new();
}

public class GetTasksResponse
{
   public IReadOnlyCollection<AlertViewModel> Alerts { get; set; }
}