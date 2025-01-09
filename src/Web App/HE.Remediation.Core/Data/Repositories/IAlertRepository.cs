using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.Data.Repositories;

public interface IAlertRepository
{
    Task<IReadOnlyCollection<GetAlertsResult>> GetAlerts(GetAlertsParameters parameters);
}