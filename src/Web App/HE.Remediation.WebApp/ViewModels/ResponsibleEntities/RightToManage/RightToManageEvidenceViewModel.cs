using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities.RightToManage;

public class RightToManageEvidenceViewModel : FileUploadViewModel
{
    public override string DeleteEndpoint => "RightToManageEvidence/Delete";
    public override string[] AcceptedFileTypes => new[] { ".pdf", ".docx", ".doc", ".xls", ".xlsx" };
    public override int NumberOfFilesAllowed => 5;
}