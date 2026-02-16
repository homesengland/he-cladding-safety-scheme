using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatories.GetGrantFundingSignatories
{
    public class GetGrantFundingSignatoriesRequest : IRequest<IReadOnlyCollection<GetGrantFundingSignatoryResponse>>
    {
        private GetGrantFundingSignatoriesRequest()
        {
        }

        public static readonly GetGrantFundingSignatoriesRequest Request = new();
    }
}
