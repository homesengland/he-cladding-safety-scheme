using System.Text.RegularExpressions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Application.Dashboard.GetPretender;

public class GetPreTenderHandler : IRequestHandler<GetPreTenderRequest, GetPreTenderResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IAlertRepository _alertRepository;
    private readonly ISystemNotificationRepository _systemNotificationRepository;
    
    private static readonly Regex MarkdownHyperlinkRegex = new(@"\[(.+?)\]\((.+?)\)", RegexOptions.Compiled);

    public GetPreTenderHandler(
        IApplicationDataProvider applicationDataProvider, 
        IAlertRepository alertRepository, 
        ISystemNotificationRepository systemNotificationRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _alertRepository = alertRepository;
        _systemNotificationRepository = systemNotificationRepository;
    }

    public async ValueTask<GetPreTenderResponse> Handle(GetPreTenderRequest request, CancellationToken cancellationToken)
    {
        var alerts = await _alertRepository.GetAlerts(new GetAlertsParameters
        {
            UserId = _applicationDataProvider.GetUserId(),
            IsAcknowledged = false
        });

        var notification = await _systemNotificationRepository.GetActiveSystemNotification();

        var message = SantiseSystemNotification(notification?.Message);

        return new GetPreTenderResponse
        {
            AlertCount = alerts.Count,
            NotificationMessage = message
        };
    }

    private string SantiseSystemNotification(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return null;
        }

        // replace angle brackets with HTML encoding to prevent HTML injection and cross site scripting
        message = message.Replace("<", "&lt;").Replace(">", "&gt;");
        // replace markdown hyperlinks with HTML anchor tags
        message = MarkdownHyperlinkRegex.Replace(message, "<a href=\"$2\" class=\"govuk-link\" rel=\"noreferrer noopener\" target=\"_blank\">$1</a>");
        return message;
    }
}
