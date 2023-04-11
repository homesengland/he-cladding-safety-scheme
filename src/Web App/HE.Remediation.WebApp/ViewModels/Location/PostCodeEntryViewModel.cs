using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Location;

public class PostCodeEntryViewModel
{
    public string PostCode { get; set; }

    public string ReturnUrl { get; set; }

    public bool NonResidentialUnits { get; set; }

    public EResponsibleEntityType ResponsibleEntityType { get; set; }
}
