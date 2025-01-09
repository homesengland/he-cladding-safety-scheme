
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonQuotesNotSought.SetReasonQuotesNotSought;

public class SetReasonQuotesNotSoughtRequest : IRequest
{
    public EWhyYouHaveNotSoughtQuotes? WhyYouHaveNotSoughtQuotes { get; set; }
    
    public string QuotesNotSoughtReason { get; set; }

    public bool? QuotesNeedsSupport { get; set; }
}

