using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.Data.Repositories
{
    public interface ITaskRepository
    {
        Task<GetTaskResult> InsertTask(InsertTaskParameters parameters);
        Task<GetTaskTypeResult> GetTaskType(GetTaskTypeParameters parameters);
    }
}
