using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.SoughtQuotes.Set;

public class SetSoughtQuotesRequest : IRequest
{
    public ENoYes? SoughtQuotes { get; set; }
}
