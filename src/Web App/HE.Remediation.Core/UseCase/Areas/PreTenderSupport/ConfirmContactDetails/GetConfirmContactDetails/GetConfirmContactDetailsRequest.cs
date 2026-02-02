
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ConfirmContactDetails.GetConfirmContactDetails;

public class GetConfirmContactDetailsRequest : IRequest<GetConfirmContactDetailsResponse>
{
    private GetConfirmContactDetailsRequest()
    {

    }

    public static readonly GetConfirmContactDetailsRequest Request = new();
}
