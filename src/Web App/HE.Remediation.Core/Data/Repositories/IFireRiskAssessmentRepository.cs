using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.Repositories;

public interface IFireRiskAssessmentRepository
{
    Task<bool?> GetHasFra(Guid applicationId);
    Task SetHasFra(SetHasFraParameters parameters);
    Task CreateFra(Guid applicationId);
    Task SetFraTaskStatus(SetFraTaskStatusParameters parameters);
    Task<GetAssessorAndFraDateResult> GetAssessorAndFraDate(Guid applicationId);
    Task SetAssessorAndFraDate(SetAssessorAndFraDateParameters parameters);
    Task<GetOtherAssessorResult> GetOtherAssessor(Guid applicationId);
    Task SetOtherAssessor(SetOtherAssessorParameters parameters);
    Task<DateTime?> GetFraDate(Guid applicationId);
    Task SetFraDate(SetFraDateParameters parameters);
    Task<IReadOnlyCollection<EInternalFireSafetyDefect>> GetInternalFireSafetyDefects(Guid applicationId);
    Task<string> GetOtherInternalSafetyRisk(Guid applicationId);
    Task SetInternalFireSafetyDefects(SetInternalFireSafetyDefectsParameters parameters);
    Task<GetFireRiskRatingResult> GetFireRiskRating(Guid applicationId);
    Task SetFireRiskRating(SetFireRiskRatingParameters parameters);
    Task<GetFraFundingResult> GetFraFunding(Guid applicationId);
    Task SetFraFunding(SetFraFundingParameters parameters);
    Task<GetFraCheckYourAnswersResult> GetFraCheckYourAnswers(Guid applicationId);
    Task<bool> GetFraVisitedCheckYourAnswers(Guid applicationId);
    Task SetFraVisitedCheckYourAnswers(SetFraVisitedCheckYourAnswersParameters parameters);
    Task SubmitFra(Guid applicationId);
    Task ClearFraAnswers(Guid applicationId);
    Task<GetFireRiskAssessmentForApplicationResult> GetFireRiskAssessmentForApplication(Guid applicationId);
    Task DeleteFraForApplication(DeleteFraForApplicationParameters parameters);
}