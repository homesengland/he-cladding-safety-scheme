using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.Application.StageDiagram.GetStageDiagram;

public class GetStageDiagramResponse
{
    public string ApplicationNumber { get; set; }

    public string UniqueBuildingName { get; set; }

    public EApplicationStage Stage { get; set; }

    public EApplicationStatus Status { get; set; }

    public DateTime DateCreated { get; set; }
}