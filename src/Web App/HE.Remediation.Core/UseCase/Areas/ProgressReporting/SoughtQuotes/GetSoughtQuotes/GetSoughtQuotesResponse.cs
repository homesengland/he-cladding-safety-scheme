
namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.SoughtQuotes.GetSoughtQuotes;

public class GetSoughtQuotesResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool? QuotesSought { get; set; }
    public int Version { get; set; }

}
