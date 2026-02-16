using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Evidence.DeleteEvidence;

public class DeleteEvidenceRequest : IRequest
{
    public Guid FileId { get; set; }

    public string ReturnUrl { get; set; }
}