using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.AddExtraContact.SetExtraContact;

public class SetExtraContactRequest : IRequest<Unit>
{
    public ENoYes? AddContact { get; set; }
}
