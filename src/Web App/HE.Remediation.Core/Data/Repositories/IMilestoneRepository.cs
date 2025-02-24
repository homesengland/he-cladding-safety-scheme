using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.Data.Repositories;

public interface IMilestoneRepository
{
    Task UpdateStartOnSiteMilestone(UpdateStartOnSiteMilestoneParameters parameters);
    Task UpdatePracticalCompletionMilestone(UpdatePracticalCompletionMilestoneParameters parameters);
    Task<GetPracticalCompletionResult> GetPracticalCompletion(Guid applicationId);
    Task<GetStartOnSiteResult> GetStartOnSite(Guid applicationId);
    Task SubmitPracticalCompletion(Guid applicationId);
    Task SubmitStartOnSite(Guid applicationId);
}