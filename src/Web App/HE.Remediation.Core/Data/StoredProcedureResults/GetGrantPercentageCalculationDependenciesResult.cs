namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetGrantPercentageCalculationDependenciesResult
{
    public int ResidentialUnitsCount { get; set; }
    public int? SharedOwnerCount { get; set; }
    public int ResponsibleEntityOrganisationTypeId { get; set; }
    public decimal? GrantPercentageOverride { get; set; }
}
