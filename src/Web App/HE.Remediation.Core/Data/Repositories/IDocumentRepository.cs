using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.Data.Repositories;

public interface IDocumentRepository
{
    Task<IReadOnlyCollection<GetApplicantDocumentsResult>> GetApplicantDocuments(GetApplicantDocumentsParameters parameters);

    Task<FileResult> GetApplicationFile(GetApplicationFileParameters parameters);
}