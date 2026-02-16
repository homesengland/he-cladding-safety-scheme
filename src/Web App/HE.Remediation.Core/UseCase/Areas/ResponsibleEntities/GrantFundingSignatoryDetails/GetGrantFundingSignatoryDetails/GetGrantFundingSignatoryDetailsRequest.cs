using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatoryDetails.GetGrantFundingSignatoryDetails
{
    public class GetGrantFundingSignatoryDetailsRequest : IRequest<GetGrantFundingSignatoryDetailsResponse>
    {
        public Guid? GrantFundingSignatoryId { get; set; }
    }
}
