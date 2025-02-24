using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ResponsibleEntityResponsibleForGrantFundingViewModel
{
    public bool? ResponsibleForGrantFunding { get; set; }

    public bool? IsClaimingGrant { get; set; }

    public bool? HasOwners { get; set; }

    public EApplicationRepresentationType RepresentationType { get; set; }

    public EApplicationResponsibleEntityOrganisationType OrganisationType { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}