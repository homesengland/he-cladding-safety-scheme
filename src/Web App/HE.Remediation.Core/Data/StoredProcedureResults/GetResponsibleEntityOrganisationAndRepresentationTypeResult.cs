using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetResponsibleEntityOrganisationAndRepresentationTypeResult
{
    public EApplicationResponsibleEntityOrganisationType OrganisationType { get; set; }
    public EApplicationRepresentationType RepresentationType { get; set; }
}