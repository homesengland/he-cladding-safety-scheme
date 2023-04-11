using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.AddExtraContact.SetExtraContact;

public class SetExtraContactRequest : IRequest<Unit>
{
    public ENoYes? AddContact { get; set; }
}
