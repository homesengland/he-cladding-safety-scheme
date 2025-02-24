using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.Location;

public class PostCodeManualViewModel : AddressViewModel
{
    public string ReturnUrl { get; set; }

    public bool NonResidentialUnits { get; set; }

    public EResponsibleEntityType ResponsibleEntityType { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public List<string> outputLocations { get; set; }    

    public List<Country> Countries { get; set; }    

    public int? CountryId { get; set; }

    public bool? UkRegistered { get; set; }
    public int ProgressReportVersion { get; set; }
    public bool IsProgressReportGcoComplete { get; set; }
}
