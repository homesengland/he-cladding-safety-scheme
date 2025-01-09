
using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Evidence.GetEvidence;

public class GetEvidenceResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public FileResult File { get; set; }
}
