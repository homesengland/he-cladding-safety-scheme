using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetUpload;

public class GetUploadRequest : IRequest<GetUploadResponse>
{
    public GetUploadRequest(EClosingReportFileType uploadType)
    {
        UploadType = uploadType;
    }

    public EClosingReportFileType UploadType { get; }
}