using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.FreeholderCompanyOrIndividual.SetFreeholderCompanyOrIndividual
{
    public class SetFreeholderCompanyOrIndividualRequest : IRequest
    {
        public EResponsibleEntityType? ReponsibleEntityType { get; set; }
    }
}
