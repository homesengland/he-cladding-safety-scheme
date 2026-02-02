using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentativeBasedInUk.SetRepresentativeBasedInUk;

public class SetRepresentativeBasedInUkRequest : IRequest
{
    public bool? BasedInUk { get; set; }
}