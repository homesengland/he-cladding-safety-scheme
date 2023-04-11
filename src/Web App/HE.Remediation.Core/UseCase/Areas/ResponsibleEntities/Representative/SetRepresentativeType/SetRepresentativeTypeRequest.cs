using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.Representative.SetRepresentativeType;

public class SetRepresentativeTypeRequest : IRequest
{
    public EApplicationRepresentationType? RepresentativeType { get; set; }
}