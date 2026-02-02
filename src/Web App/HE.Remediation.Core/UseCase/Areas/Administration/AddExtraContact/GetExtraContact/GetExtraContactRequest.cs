using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.AddExtraContact.GetExtraContact;

public class GetExtraContactRequest: IRequest<GetExtraContactResponse>
{
    private GetExtraContactRequest()
    {
    }

    public static readonly GetExtraContactRequest Request = new();
}
