using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.DeleteFile;

public class DeleteFileRequest : IRequest<Unit>
{
    public Guid FileId { get; set; }
}
