using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class SystemNotificationRepository : ISystemNotificationRepository
{
    private readonly IDbConnectionWrapper _connection;

    public SystemNotificationRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<GetActiveSystemNotificationResult> GetActiveSystemNotification()
    {
        var notification = await _connection.QuerySingleOrDefaultAsync<GetActiveSystemNotificationResult>(nameof(GetActiveSystemNotification));
        return notification;
    }
}