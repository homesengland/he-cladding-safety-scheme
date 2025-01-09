using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Helpers;

public class CalculateWorkPackageGrantPercentageRequest
{
    public EApplicationResponsibleEntityOrganisationType ResponsibleEntityOrganisationType { get; set; }
    
    public decimal? GrantPercentageOverride { get; set; }

    public int ResidentialUnitCount { get; set; }
    
    public int? SharedOwnerCount { get; set; }

    public decimal? TotalGrantFunding { get; set; }
}

