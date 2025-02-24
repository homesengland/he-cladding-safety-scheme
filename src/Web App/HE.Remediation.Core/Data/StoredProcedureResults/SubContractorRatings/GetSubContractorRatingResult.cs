namespace HE.Remediation.Core.Data.StoredProcedureResults.SubContractorRatings;

public class GetSubContractorRatingResult
{
    public Guid Id { get; set; }
    public int? QualityOfWork { get; set; }
    public int? ValueForMoney { get; set; }
    public int? Reliability { get; set; }
    public int? ConsiderationOfResidents { get; set; }
    public int? OverallSatisfaction { get; set; }
    public string CompanyName { get; set; }
    public string CompanyRegistrationNumber { get; set; }
}