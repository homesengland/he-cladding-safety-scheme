using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class GrantFundingSignatoryViewModel
{
    public Guid Id { get; set; }

    public string FullName { get; set; }

    public string EmailAddress { get; set; }

    public string Role { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}