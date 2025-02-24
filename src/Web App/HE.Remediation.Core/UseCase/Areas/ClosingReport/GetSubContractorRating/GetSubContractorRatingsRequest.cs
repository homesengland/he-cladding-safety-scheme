using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubContractorRating;

public class GetSubContractorRatingsRequest : IRequest<GetSubContractorRatingsResponse>
{
    public Guid Id { get; set; }
    
    public GetSubContractorRatingsRequest(Guid id)
    {
        Id = id;
    }
}
