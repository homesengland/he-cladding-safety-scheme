using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityUkRegistered.SetResponsibleEntityUkRegistered
{
    public class SetResponsibleEntityUkRegisteredResponse
    {
        public bool? HasRepresentative { get; set; }
        public bool? HasValidOrganisationTypes { get; set; }
        public EApplicationResponsibleEntityOrganisationType? OrganisationType { get; set; }
        public bool? HasRepresentativeUkBased { get; set; }
    }
}
