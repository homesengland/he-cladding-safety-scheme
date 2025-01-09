using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetUpload;

public class GetUploadResponse
{
	public EClosingReportFileType UploadType { get; set; }
	public IReadOnlyCollection<FileResult> AddedFiles { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
}