﻿
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonQuotesNotSought.GetReasonQuotesNotSought;

public class GetReasonQuotesNotSoughtResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public EWhyYouHaveNotSoughtQuotes? WhyYouHaveNotSoughtQuotes { get; set; }

    public string QuotesNotSoughtReason { get; set; }

    public bool? QuotesNeedsSupport { get; set; }

    public int Version { get; set; }
}
