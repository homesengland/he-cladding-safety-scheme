using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class DateRepository : IDateRepository
{
    private readonly IDbConnectionWrapper _connection;

    public DateRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<DateTime> AddWorkingDays(AddWorkingDaysParameters parameters)
    {
        var date = await _connection.QuerySingleOrDefaultAsync<DateTime>(nameof(AddWorkingDays), parameters);
        return date;
    }
}