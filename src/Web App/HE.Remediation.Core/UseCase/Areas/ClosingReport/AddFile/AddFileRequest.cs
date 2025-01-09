using HE.Remediation.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.AddFile;

public class AddFileRequest : IRequest<Unit>
{
    public EClosingReportFileType UploadType { get; set; }
    public IFormFile File { get; set; }
}
