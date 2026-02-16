using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubContractorRating;

public class GetSubContractorRatingsRequest : IRequest<GetSubContractorRatingsResponse>
{
    public Guid Id { get; set; }
    
    public GetSubContractorRatingsRequest(Guid id)
    {
        Id = id;
    }
}
