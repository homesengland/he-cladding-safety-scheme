using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.Profile.SetUserResponsibleEntityType;

public class SetUserResponsibleEntityTypeRequest : IRequest
{
    public EResponsibleEntityType ResponsibleEntityType { get; set; }
}