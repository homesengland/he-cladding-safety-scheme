using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureParameters.ResponsibleEntities
{
    public class SetResponsibleEntityCompanyRelationDetailsParameters
    {
        public Guid ApplicationId { get; set; }
        public int ResponsibleEntityRelationId { get; set; }
    }
}
