
namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetSignatoryResult
{
    public Guid Id { get; set; }
    public string FullName { get; set; }        
    public string Role { get; set; }
    public string EmailAddress { get; set; }
}
