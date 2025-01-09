using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Delete;

public class DeleteEvidenceRequest : IRequest
{
    public Guid FileId { get; set; }
}

