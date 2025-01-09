
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class ProgressReportQuotesNotSoughtReasonResult
{
    public EWhyYouHaveNotSoughtQuotes? WhyYouHaveNotSoughtQuotes { get; set; }
    public string QuotesNotSoughtReason { get; set; }

    public bool? QuotesNeedsSupport { get; set; }
}
