using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetResponsibleEntityCompanyTypeResult
{
    public EApplicationResponsibleEntityOrganisationType? OrganisationType { get; set; }
    public EApplicationResponsibleEntityOrganisationSubType? OrganisationSubType { get; set; }
    public string OrganisationSubTypeDescription { get; set; }
    public EResponsibleEntityRelation? ResponsibleEntityRelationType { get; set; }
}