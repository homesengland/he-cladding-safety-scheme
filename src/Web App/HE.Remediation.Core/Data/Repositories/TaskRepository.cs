using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbConnectionWrapper _connection;

        public TaskRepository(IDbConnectionWrapper dbConnectionWrapper)
        {
                _connection = dbConnectionWrapper;
        }
        public async Task<GetTaskResult> InsertTask(InsertTaskParameters parameters)
        {
            return await _connection
                .QuerySingleOrDefaultAsync<GetTaskResult>(
                    nameof(InsertTask),
                    parameters);
        }

        public async Task<GetTaskTypeResult> GetTaskType(GetTaskTypeParameters parameters)
        {
            return await _connection
                .QuerySingleOrDefaultAsync<GetTaskTypeResult>(
                    nameof(GetTaskType),
                    parameters);
        }
    }
}
