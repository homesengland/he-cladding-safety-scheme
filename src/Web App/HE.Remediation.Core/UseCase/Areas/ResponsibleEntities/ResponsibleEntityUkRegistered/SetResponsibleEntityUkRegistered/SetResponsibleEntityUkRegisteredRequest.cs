using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityUkRegistered.SetResponsibleEntityUkRegistered;

public class SetResponsibleEntityUkRegisteredRequest : IRequest<SetResponsibleEntityUkRegisteredResponse>
{
    public bool? UkRegistered { get; set; }
}