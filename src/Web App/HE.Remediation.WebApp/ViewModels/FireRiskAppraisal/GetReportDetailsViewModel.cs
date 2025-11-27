
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class GetReportDetailsViewModel
{
    public EApplicationScheme ApplicationScheme { get; set; }
    public string AuthorsName { get; set; }
    public string PeerReviewPerson { get; set; }
    public decimal? FraewCost { get; set; }
    public int? NumberOfStoreys { get; set; }    
    public decimal? BuildingHeight { get; set; }
    public EBasicComplexType BasicComplexId { get; set; }
    public string BuildingAddress { get; set; }
    public DateTime? FRAEWInstructedDate { get; set; }
    public string BuildingName { get; set; }
    public DateTime? FRAEWCompletedDate { get; set; }
    public string CompanyUndertakingReport { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public bool? PartOfDevelopment { get; set; }
    public string Development { get; set; }

    public string ReturnUrl { get; set; }
}
