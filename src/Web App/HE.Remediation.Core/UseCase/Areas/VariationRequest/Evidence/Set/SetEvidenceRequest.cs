using Mediator;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Set;

public class SetEvidenceRequest : IRequest<Unit>
{
    public IFormFile File { get; set; }
    public bool Completed { get; set; }
}
