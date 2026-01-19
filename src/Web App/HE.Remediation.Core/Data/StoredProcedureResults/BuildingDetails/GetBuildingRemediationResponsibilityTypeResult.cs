using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.BuildingDetails;

public class GetBuildingRemediationResponsibilityTypeResult
{
    public EBuildingRemediationResponsibilityType? BuildingRemediationResponsibilityTypeId { get; set; }
    public bool? BuildingHasSafetyRegulatorRegistrationCode { get; set; }
}