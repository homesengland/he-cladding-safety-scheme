using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepCompanyOrIndividual.SetRepCompanyOrIndividual;

public class SetRepCompanyOrIndividualRequest : IRequest
{
    public EResponsibleEntityType? ReponsibleEntityType { get; set; }
}