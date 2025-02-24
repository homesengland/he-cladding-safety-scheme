using MediatR;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Add;

public class AddEvidenceRequest : IRequest<Unit>
{
    public IFormFile File { get; set; }
    public bool Completed { get; set; }
}
