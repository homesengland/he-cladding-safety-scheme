using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.SoughtQuotes.GetSoughtQuotes;

public class GetSoughtQuotesRequest : IRequest<GetSoughtQuotesResponse>
{
    private GetSoughtQuotesRequest()
    {
    }

    public static readonly GetSoughtQuotesRequest Request = new();
}
