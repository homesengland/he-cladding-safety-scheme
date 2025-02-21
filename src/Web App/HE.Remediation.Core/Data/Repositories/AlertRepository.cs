using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class AlertRepository : IAlertRepository
{
    private readonly IDbConnectionWrapper _connection;

    public AlertRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<IReadOnlyCollection<GetAlertsResult>> GetAlerts(GetAlertsParameters parameters)
    {
        var alerts = await _connection.QueryAsync<GetAlertsResult>(nameof(GetAlerts), parameters);
        return alerts;
    }

    public async Task InsertAlert(InsertAlertParameters parameters)
    {
        await _connection.ExecuteAsync("InsertAlert", parameters);
    }
}