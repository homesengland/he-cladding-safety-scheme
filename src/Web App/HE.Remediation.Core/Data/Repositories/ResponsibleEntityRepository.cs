using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class ResponsibleEntityRepository : IResponsibleEntityRepository
{
    private readonly IDbConnectionWrapper _connection;

    public ResponsibleEntityRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<GetResponsibleEntityCompanyTypeResult> GetResponsibleEntityCompanyType(Guid applicationId)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetResponsibleEntityCompanyTypeResult>(
            "GetResponsibleEntityCompanyType", new
            {
                ApplicationId = applicationId
            });

        return result;
    }
}