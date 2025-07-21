namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetDeclaration;

public class GetDeclarationResponse
{
    public DateTime? DateOfCompletion { get; set; }

    public bool? FraewRiskToLifeReduced { get; set; }

    public bool? GrantFundingObligations { get; set; }

    public DateTime? ApplicationCreationDate { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }
}