using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetUpload;

public class GetUploadRequest : IRequest<GetUploadResponse>
{
    public GetUploadRequest(EClosingReportFileType uploadType)
    {
        UploadType = uploadType;
    }

    public EClosingReportFileType UploadType { get; }
}