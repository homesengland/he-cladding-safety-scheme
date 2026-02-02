using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.Profile.SetUserResponsibleEntityType;

public class SetUserResponsibleEntityTypeRequest : IRequest
{
    public EResponsibleEntityType ResponsibleEntityType { get; set; }
}