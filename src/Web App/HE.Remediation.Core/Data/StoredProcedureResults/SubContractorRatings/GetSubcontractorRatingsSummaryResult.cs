namespace HE.Remediation.Core.Data.StoredProcedureResults.SubContractorRatings;

public class GetSubcontractorRatingsSummaryResult
{
    public Guid Id { get; set; }
    public int QualityOfWork { get; set; }
    public int ValueForMoney { get; set; }
    public int Reliability { get; set; }
    public int ConsiderationOfResidents { get; set; }
    public int OverallSatisfaction { get; set; }
    public string CompanyName { get; set; }
}