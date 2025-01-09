using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetDeclaration;

public class GetDeclarationResponse
{
    public DateTime? DateOfCompletion { get; set; }

    public ERiskType? LifeSafetyRiskAssessment { get; set; }

    public DateTime? ApplicationCreationDate { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
}
