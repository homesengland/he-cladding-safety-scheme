using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport;

public class GetProgressReportDataForTasksResult
{
    public bool? LeadDesignerNeedsSupport { get; set; }
    public bool? OtherMembersNeedsSupport { get; set; }
    public bool? PlanningPermissionNeedsSupport { get; set; }
    public bool? QuotesNeedsSupport { get; set; }
    public bool? OtherNeedsSupport { get; set; }
    public string SupportNeededReason { get; set; }
    public DateTime? EstimatedWorksPackagCompletionDate { get; set; }
    public DateTime? GfaCompletionDate { get; set; }
    public bool? EnoughFunds { get; set; }
    public EContractorTenderType? ContractorTenderTypeId { get; set; }
    public bool? HasGco { get; set; }
    public bool? DutyOfCareDeedTaskRaised { get; set; }

    public bool HasSupportNeeds => LeadDesignerNeedsSupport == true
                                   || OtherMembersNeedsSupport == true
                                   || PlanningPermissionNeedsSupport == true
                                   || QuotesNeedsSupport == true
                                   || OtherNeedsSupport == true;

    public bool HasNineMonthGap => EstimatedWorksPackagCompletionDate.HasValue
    && GfaCompletionDate.HasValue
    && EstimatedWorksPackagCompletionDate.Value > GfaCompletionDate.Value.AddMonths(9);
}