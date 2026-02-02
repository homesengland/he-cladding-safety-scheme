using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepCompanyOrIndividual.SetRepCompanyOrIndividual;

public class SetRepCompanyOrIndividualRequest : IRequest
{
    public EResponsibleEntityType? ReponsibleEntityType { get; set; }
}