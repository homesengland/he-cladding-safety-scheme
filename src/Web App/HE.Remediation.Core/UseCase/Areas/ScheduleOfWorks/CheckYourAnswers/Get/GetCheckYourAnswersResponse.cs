namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.CheckYourAnswers.Get;

public class GetCheckYourAnswersResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public DateTime? ProjectStartDate { get; set; }

    public DateTime? ProjectEndDate { get; set; }

    public decimal? ApprovedGrantFunding { get; set; }

    public decimal? GrantPaidToDate { get; set; }

    public decimal? ProfiledPayments { get; set; }

    public int? Duration { get; set; }

    public bool IsSubmitted { get; set; }

    public IReadOnlyCollection<string> ContractFileNames { get; set; }
    public IReadOnlyCollection<string> BuildingControlFileNames { get; set; }
    public IReadOnlyCollection<string> LeaseholderEngagementFileNames { get; set; }
}
