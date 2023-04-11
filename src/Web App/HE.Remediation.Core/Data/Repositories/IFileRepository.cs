using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.Data.Repositories;

public interface IFileRepository
{
    Task InsertFile(InsertFileParameters parameters);
    Task<DeleteFileResult> DeleteFile(Guid fileId);
}