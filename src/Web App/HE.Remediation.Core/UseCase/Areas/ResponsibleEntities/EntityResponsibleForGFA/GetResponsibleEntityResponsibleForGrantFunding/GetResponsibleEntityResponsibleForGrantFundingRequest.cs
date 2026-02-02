using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.EntityResponsibleForGFA.GetResponsibleEntityResponsibleForGrantFunding
{
    public class GetResponsibleEntityResponsibleForGrantFundingRequest : IRequest<GetResponsibleEntityResponsibleForGrantFundingResponse>
    {
        private GetResponsibleEntityResponsibleForGrantFundingRequest()
        {
        }

        public static readonly GetResponsibleEntityResponsibleForGrantFundingRequest Request = new();
    }
}
