namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class UpdateStartOnSiteMilestoneParameters
{
    public Guid ApplicationId { get; set; }
    public DateTime StartOnSiteDate { get; set; }
}