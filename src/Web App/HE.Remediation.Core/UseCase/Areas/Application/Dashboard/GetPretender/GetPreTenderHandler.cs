using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.Dashboard.GetPretender;

public class GetPreTenderHandler : IRequestHandler<GetPreTenderRequest, GetPreTenderResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IAlertRepository _alertRepository;
    private readonly ISystemNotificationRepository _systemNotificationRepository;

    public GetPreTenderHandler(
        IApplicationDataProvider applicationDataProvider, 
        IAlertRepository alertRepository, 
        ISystemNotificationRepository systemNotificationRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _alertRepository = alertRepository;
        _systemNotificationRepository = systemNotificationRepository;
    }

    public async Task<GetPreTenderResponse> Handle(GetPreTenderRequest request, CancellationToken cancellationToken)
    {
        var alerts = await _alertRepository.GetAlerts(new GetAlertsParameters
        {
            UserId = _applicationDataProvider.GetUserId(),
            IsAcknowledged = false
        });

        var notification = await _systemNotificationRepository.GetActiveSystemNotification();

        return new GetPreTenderResponse
        {
            AlertCount = alerts.Count,
            NotificationMessage = notification?.Message
        };
    }
}
