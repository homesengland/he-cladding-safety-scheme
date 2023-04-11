using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Application;

public class StageDiagramViewModel
{
    public string ApplicationNumber { get; set; }

    public string UniqueBuildingName { get; set; }

    public EApplicationStage Stage { get; set; }

    public EApplicationStatus Status { get; set; }

    public DateTime DateCreated { get; set; }
}