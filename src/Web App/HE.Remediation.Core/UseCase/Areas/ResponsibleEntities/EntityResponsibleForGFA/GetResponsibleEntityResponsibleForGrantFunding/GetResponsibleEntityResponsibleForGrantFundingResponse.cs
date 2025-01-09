using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.EntityResponsibleForGFA.GetResponsibleEntityResponsibleForGrantFunding
{
    public class GetResponsibleEntityResponsibleForGrantFundingResponse
    {
        public bool? ResponsibleForGrantFunding { get; set; }

        public bool? IsClaimingGrant { get; set; }

        public bool? HasOwners { get; set; }

        public EApplicationRepresentationType OrganisationType { get; set; }

        public EApplicationRepresentationType RepresentationType { get; set; }
    }
}
