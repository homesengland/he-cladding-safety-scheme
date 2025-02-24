using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetCheckYourAnswers;

public class GetCheckYourAnswersResponse
{
    public string PaymentRequestName { get; set; }

    public bool? CostsChanged { get; set; }

    public bool? ThirdPartyContributionsChanged { get; set; }

    public string ReasonForChange { get; set; }

    public bool? UnsafeCladdingRemoved { get; set; }    
    public DateTime? UnsafeCladdingRemovedDate { get; set; }

    public DateTime? ExpectedStartDate { get; set; }
    public DateTime? ExpectedEndDate { get; set; }

    public decimal? ScheduledAmount { get; set; }

    public bool? ProjectDatesChanged { get; set; }

    public decimal? CurrentMonthCost { get; set; }

    public string AdditionalCostMonthTitle { get; set; }

    public decimal? AdditionalCostAmount { get; set; }
    
    public List<PaymentRequestCostFile> PaymentRequestCostFiles { get; set; }
    public IList<string> PaymentRequestInvoiceFileNames { get; set; }

    public List<GetTeamMembersResult> TeamMembers { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }

    public bool IsExpired { get; set; }

    public bool UnsafeCladdingAlreadyRemoved { get; set; }
}

public class PaymentRequestCostFile
{
    public string Name { get; set; }
}