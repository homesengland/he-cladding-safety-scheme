
namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ReportDetails.GetReportDetails;

public class FRAEWBuildingDetails
{
    public string BuildingAddress { get; set; }
    public DateTime? FRAEWInstructedDate { get; set; }
    public string BuildingName { get; set; }
    public DateTime? FRAEWCompletedDate { get; set; }
    public string CompanyUndertakingReport { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public bool? PartOfDevelopment { get; set; }
    public string Development { get; set; }
}
