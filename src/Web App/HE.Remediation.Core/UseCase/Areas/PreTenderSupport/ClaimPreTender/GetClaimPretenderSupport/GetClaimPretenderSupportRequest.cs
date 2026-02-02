using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ClaimPreTender.GetClaimPretenderSupport;

public class GetClaimPretenderSupportRequest: IRequest<GetClaimPretenderSupportResponse>
{
    private GetClaimPretenderSupportRequest()
    {

    }

    public static readonly GetClaimPretenderSupportRequest Request = new();
}
