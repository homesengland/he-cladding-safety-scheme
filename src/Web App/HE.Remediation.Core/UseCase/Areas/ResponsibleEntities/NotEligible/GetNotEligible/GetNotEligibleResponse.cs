using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.NotEligible.GetNotEligible;

public class GetNotEligibleResponse
{
    public bool? IsUkBased { get; set; }
    public EApplicationRepresentationType? RepresentationType { get; set; }
    public EApplicationResponsibleEntityOrganisationType? CompanyType { get; set; }
    public bool? IsUkRegistered { get; set; }
    public bool? HasOwners { get; set; }
    public bool? IsClaimingGrant { get; set; }
    public bool? HasAcquiredRightToManage { get; set; }
    public EApplicationScheme ApplicationScheme { get; set; }
    public EApplicationStatus StatusId { get; set; }
}