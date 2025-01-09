namespace HE.Remediation.Core.Helpers;

public class CalculateWorkPackageGrantPercentageResponse
{
    public int TotalUnits { get; set; }

    public int EligibleUnits { get; set; }

    public decimal Percentage { get; set; }

    public decimal GrantAmount { get; set; }
}

