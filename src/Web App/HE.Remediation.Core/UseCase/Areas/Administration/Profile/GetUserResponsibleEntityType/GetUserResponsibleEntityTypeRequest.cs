using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.Profile.GetUserResponsibleEntityType;

public class GetUserResponsibleEntityTypeRequest : IRequest<EResponsibleEntityType?>
{
    public static readonly GetUserResponsibleEntityTypeRequest Request = new();
}