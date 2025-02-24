using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.NoQuotes.Get;

public class GetNoQuotesRequest : IRequest<GetNoQuotesResponse>
{
    private GetNoQuotesRequest()
    {
    }

    public static GetNoQuotesRequest Request => new();
}
