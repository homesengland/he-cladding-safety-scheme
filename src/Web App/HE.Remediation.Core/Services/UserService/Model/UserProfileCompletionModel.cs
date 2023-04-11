using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Services.UserService.Model;

public record UserProfileCompletionModel
{
    public EResponsibleEntityType ResponsibleEntityType { get; set; }
    public bool IsContactInformationComplete { get; set; }
    public bool? IsCorrespondenceAddressComplete { get; set; }
    public bool IsResponsibleEntityTypeSelectionComplete { get; set; }
    public bool? IsCompanyDetailsComplete { get; set; }
    public bool? IsCompanyAddressComplete { get; set; }
    public bool? IsSecondaryContactInformationComplete { get; set; }    
    public bool? IsSecondaryContactSelectionComplete { get; set; }    

    public bool IsContactConsentComplete { get; set; }    

    public bool? IsWantSecondaryContactComplete { get; set; }    

    public bool? WantedToAddSecondaryContact { get; set; }
};