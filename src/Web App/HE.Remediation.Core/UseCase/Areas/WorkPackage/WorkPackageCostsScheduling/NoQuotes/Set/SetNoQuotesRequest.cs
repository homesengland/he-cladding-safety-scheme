using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.NoQuotes.Set;

public class SetNoQuotesRequest : IRequest
{
    public string Reason { get; set; }
}
