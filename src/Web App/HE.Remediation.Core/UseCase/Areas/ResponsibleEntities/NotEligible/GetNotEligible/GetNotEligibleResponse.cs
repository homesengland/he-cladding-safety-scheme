using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.NotEligible.GetNotEligible;

public class GetNotEligibleResponse
{
    public bool? IsUkBased { get; set; }
    public EApplicationRepresentationType RepresentationType { get; set; }
}