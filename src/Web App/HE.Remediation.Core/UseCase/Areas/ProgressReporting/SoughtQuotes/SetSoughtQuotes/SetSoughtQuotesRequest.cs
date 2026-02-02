
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.SoughtQuotes.SetSoughtQuotes;

public class SetSoughtQuotesRequest : IRequest
{
    public bool? QuotesSought { get; set; }
}
