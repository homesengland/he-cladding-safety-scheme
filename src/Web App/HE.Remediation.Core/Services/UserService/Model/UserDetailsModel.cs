using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Services.UserService.Model;

public record UserDetailsModel
{
    public Guid UserId { get; set; }
    public string Auth0UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactNumber { get; set; }
    
    public string EmailAddress { get; set; }
    public DateTime Created { get; set; }
    
    public EResponsibleEntityType? ResponsibleEntityType { get; set; }
    public DateTime? LastLoginTime { get; set; }
    public int LoginCount { get; set; }
}