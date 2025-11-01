using Dapper;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.Core.Data.Repositories;
public class WorksPackageMemorandumRepository : IWorksPackageMemorandumRepository
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IStatusTransitionService _statusTransitionService;
    private readonly ILogger<WorksPackageMemorandumRepository> _logger;

    public WorksPackageMemorandumRepository(IDbConnectionWrapper connection, 
        IApplicationDataProvider applicationDataProvider, 
        IStatusTransitionService statusTransitionService,
        ILogger<WorksPackageMemorandumRepository> logger)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
        _statusTransitionService = statusTransitionService;
        _logger = logger;
    }

    public async Task CreateWorkPackageMemorandum(Guid applicationId)
    {
        _logger.LogDebug("Executing stored procedure: {spName}", nameof(CreateWorkPackageMemorandum));

        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        await _connection.ExecuteAsync(nameof(CreateWorkPackageMemorandum), parameters);
    }
}
