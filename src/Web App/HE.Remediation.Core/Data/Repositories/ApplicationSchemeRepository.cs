using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Application.Dashboard.SchemeSelection;

namespace HE.Remediation.Core.Data.Repositories;

public class ApplicationSchemeRepository : IApplicationSchemeRepository
{
    private readonly IDbConnectionWrapper _connection;

    public ApplicationSchemeRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<IReadOnlyCollection<ApplicationScheme>> GetApplicationSchemes()
    {
        var applicationSchemes = await _connection.QueryAsync<ApplicationScheme>(nameof(GetApplicationSchemes));
        return applicationSchemes;
    }
}