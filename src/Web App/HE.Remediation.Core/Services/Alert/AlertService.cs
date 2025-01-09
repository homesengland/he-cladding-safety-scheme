using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Services.Alert.Managers;
using HE.Remediation.Core.Services.Alert.Models;

namespace HE.Remediation.Core.Services.Alert;

public class AlertService : IAlertService
{
    private readonly IEnumerable<IAlertManager> _alertManagers;

    public AlertService(IEnumerable<IAlertManager> alertManagers)
    {
        _alertManagers = alertManagers;
    }
    
    public async Task<AlertViewModel> CreateAlertModel(GetAlertsResult alert)
    {
        var manager = _alertManagers.FirstOrDefault(x => x.AlertType == (EAlertType)alert.AlertTypeId);
        var title = manager is not null 
            ? await manager.GetTitle(alert) 
            : string.Empty;

        return new AlertViewModel
        {
            AlertId = alert.Id,
            IsAcknowledged = alert.Acknowledged,
            DateCreated = alert.CreationDate,
            Title = title
        };
    }

    public async Task<IEnumerable<AlertViewModel>> CreateAlertModels(IEnumerable<GetAlertsResult> alerts)
    {
        var models = new List<AlertViewModel>();

        foreach (var alert in alerts)
        {
            var model = await CreateAlertModel(alert);
            if (model is not null)
            {
                models.Add(model);
            }
        }

        return models;
    }
}