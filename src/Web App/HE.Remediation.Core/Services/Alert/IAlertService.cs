using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Services.Alert.Models;

namespace HE.Remediation.Core.Services.Alert;

public interface IAlertService
{
    Task<AlertViewModel> CreateAlertModel(GetAlertsResult alert);
    Task<IEnumerable<AlertViewModel>> CreateAlertModels(IEnumerable<GetAlertsResult> alerts);
}