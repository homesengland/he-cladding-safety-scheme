
namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetClosingReportInformation;

public class GetClosingReportInformationResponse
{
    public bool IsSubmitted { get; set; }  
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
}
