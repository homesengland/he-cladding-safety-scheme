using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Get;

public class GetWorksContractResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }

    public IReadOnlyCollection<FileResult> AddedFiles { get; set; }
}
