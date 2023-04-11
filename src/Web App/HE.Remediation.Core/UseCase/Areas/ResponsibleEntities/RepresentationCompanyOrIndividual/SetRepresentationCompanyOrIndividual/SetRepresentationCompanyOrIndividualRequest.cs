using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentationCompanyOrIndividual.SetRepresentationCompanyOrIndividual;

public class SetRepresentationCompanyOrIndividualRequest : IRequest
{
    public EResponsibleEntityType? ReponsibleEntityType { get; set; }
}