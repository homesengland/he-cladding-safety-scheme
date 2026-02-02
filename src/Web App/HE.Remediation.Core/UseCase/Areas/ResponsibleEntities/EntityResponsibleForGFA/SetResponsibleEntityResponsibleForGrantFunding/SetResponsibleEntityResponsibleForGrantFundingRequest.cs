using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.EntityResponsibleForGFA.SetResponsibleEntityResponsibleForGrantFunding
{
    public class SetResponsibleEntityResponsibleForGrantFundingRequest : IRequest<SetResponsibleEntityResponsibleForGrantFundingResponse>
    {
        public bool? ResponsibleForGrantFunding { get; set; }
    }
}
