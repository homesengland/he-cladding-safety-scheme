using HE.Remediation.Core.Enums;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.UploadFileEvidence;

public class DeleteThirdPartyEvidenceFileRequest(Guid id, Guid applicationId, Guid fileId) : IRequest<Unit>
{
    public Guid Id { get; } = id;
    public Guid ApplicationId { get; } = applicationId;
    public Guid FileId { get; } = fileId;
}
