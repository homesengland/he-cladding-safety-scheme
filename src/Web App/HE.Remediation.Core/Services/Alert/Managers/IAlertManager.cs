using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Services.Alert.Models;

namespace HE.Remediation.Core.Services.Alert.Managers;

public interface IAlertManager
{
    EAlertType AlertType { get; }

    Task<string> GetTitle(GetAlertsResult alert);
}