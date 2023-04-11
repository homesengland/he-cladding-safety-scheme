using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class UploadEvidenceViewModel : FileUploadViewModel
{
    public EApplicationResponsibleEntityOrganisationType OrganisationType { get; set; }
    public override string DeleteEndpoint => "/ResponsibleEntities/UploadEvidence/Delete";
    public override string[] AcceptedFileTypes => new[] { ".pdf" };
    public override int NumberOfFilesAllowed => 5;

    public string ReturnUrl { get; set; }
}