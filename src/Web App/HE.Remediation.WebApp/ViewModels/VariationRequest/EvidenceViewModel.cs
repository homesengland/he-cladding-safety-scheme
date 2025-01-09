using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;
using File = HE.Remediation.WebApp.ViewModels.Shared.File;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class EvidenceViewModel : VariationRequestBaseViewModel
{
    public IFormFile File { get; set; }

    public List<File> AddedFiles { get; set; }

    public string DeleteEndpoint => "/VariationRequest/Evidence/Delete";

    public string[] AcceptedFileTypes => new[] { ".pdf", ".xlsx", ".jpg", ".jpeg", ".png" };

    public int NumberOfFilesAllowed => 5;

    public ENoYes? IneligibleCosts { get; set; }

    public bool? IsThirdPartyContributionVariation { get; set; }
}
