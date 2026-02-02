using HE.Remediation.Core.Enums;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.UploadFileEvidence;

public class AddThirdPartyEvidenceFileRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid ApplicationId { get; set; }
    public IFormFile File { get; set; }
}
