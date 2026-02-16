using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.DeleteFile;

public class DeleteFileRequest : IRequest<Unit>
{
    public Guid FileId { get; set; }
    public EClosingReportTask Task { get; set; }
}
