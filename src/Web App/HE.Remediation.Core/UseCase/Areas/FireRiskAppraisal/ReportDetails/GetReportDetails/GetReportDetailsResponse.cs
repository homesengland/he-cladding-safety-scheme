
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ReportDetails.GetReportDetails;

public class GetReportDetailsResponse
{
    public string AuthorsName { get; set; }
    public string PeerReviewPerson { get; set; }
    public string UndertakingFirm { get; set; }
    public int? NumberOfStoreys { get; set; }    

    public int? BuildingHeight { get; set; }

    public ENoYes? BuildingInterimMeasures { get; set; }

    public EBasicComplexType BasicComplexId { get; set; }

    public string BuildingAddress { get; set; }

    public DateTime? FRAEWInstructedDate { get; set; }

    public string BuildingName { get; set; }

    public DateTime? FRAEWCompletedDate { get; set; }    

    public string CompanyUndertakingReport { get; set; }
}
