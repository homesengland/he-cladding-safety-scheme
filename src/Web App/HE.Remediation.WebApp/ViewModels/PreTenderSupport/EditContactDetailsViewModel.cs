namespace HE.Remediation.WebApp.ViewModels.PreTenderSupport;

public class EditContactDetailsViewModel
{
    public string ReturnUrl { get; set; }

    public Guid Id { get; set; }
    public string FullName { get; set; }    
    public string Role { get; set; }
    public string EmailAddress { get; set; }
    public bool IsSubmitted { get; set; }
}
