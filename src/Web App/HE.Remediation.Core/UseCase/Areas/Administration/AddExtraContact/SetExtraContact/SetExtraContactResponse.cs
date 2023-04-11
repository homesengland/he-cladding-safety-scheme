using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.AddExtraContact.SetExtraContact;

public class SetExtraContactResponse: IRequest<Unit>
{
    public ENoYes? AddContact { get; set; }
}
