
namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.EmailContactDetails;

public class GetEmailContactDetailsResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
    public string EmailAddress { get; set; }
    public bool IsSubmitted { get; set; }
}
