namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetFireRiskAssessorPdfListResult
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string EmailAddress { get; set; }
    public string Telephone { get; set; }
    public IList<RegionResult> Regions { get; set; } = new List<RegionResult>();

    public class RegionResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}