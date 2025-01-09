using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.SubContractorRatings;

public class GetSubContractorRatingsOverviewResult
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; }
    public string CompanyRegistrationNumber { get; set; }
    public ETaskStatus Status { get; set; }
}