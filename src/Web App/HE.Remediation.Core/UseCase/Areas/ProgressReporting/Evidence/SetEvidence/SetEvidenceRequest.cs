using Mediator;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Evidence.SetEvidence;

public class SetEvidenceRequest : IRequest
{
    public IFormFile File { get; set; }
}
