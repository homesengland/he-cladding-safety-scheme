using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.SoughtQuotes.Get;

public class GetSoughtQuotesRequest : IRequest<GetSoughtQuotesResponse>
{
    private GetSoughtQuotesRequest()
    {
    }

    public static GetSoughtQuotesRequest Request => new();
}
