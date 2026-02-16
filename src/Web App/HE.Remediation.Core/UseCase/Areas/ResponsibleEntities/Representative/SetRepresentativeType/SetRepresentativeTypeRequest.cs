using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.Representative.SetRepresentativeType;

public class SetRepresentativeTypeRequest : IRequest
{
    public EApplicationRepresentationType? RepresentativeType { get; set; }
}