using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.Repositories;

public interface IWorkPackageFireRiskAssessmentRepository
{
    Task InsertWorkPackageFra(Guid applicationId);
    Task<GetWorkPackageFraReportResult> GetWorkPackageFraReport(Guid applicationId);
    Task UploadWorkPackageFraReport(UploadWorkPackageFraReportParameters parameters);
    Task DeleteWorkPackageFraReport(DeleteWorkPackageFraReportParameters parameters);
    Task<bool> GetWorkPackageFraVisitedCheckYourAnswers(Guid applicationId);
    Task SetWorkPackageFireRiskAssessmentType(SetWorkPackageFireRiskAssessmentType parameters);
    Task<GetWorkPackageFraAssessorAndDateResult> GetWorkPackageFraAssessorAndDate(Guid applicationId);
    Task SetWorkPackageFraAssessorAndDate(SetWorkPackageFraAssessorAndDateParameters parameters);
    Task<GetWorkPackageFraOtherAssessorResult> GetWorkPackageFraOtherAssessor(Guid applicationId);
    Task SetWorkPackageFraOtherAssessor(SetWorkPackageFraOtherAssessorParameters parameters);
    Task<DateTime?> GetWorkPackageFraDate(Guid applicationId);
    Task SetWorkPackageFraDate(SetWorkPackageFraDateParameters parameters);
    Task<GetWorkPacakgeFraFireRiskRatingResult> GetWorkPacakgeFraFireRiskRating(Guid applicationId);
    Task SetWorkPackageFraFireRiskRating(SetWorkPackageFraFireRiskRatingParameters parameters);
    Task<IReadOnlyCollection<EInternalFireSafetyDefect>> GetWorkPackageFraInternalDefects(Guid applicationId);
    Task SetWorkPackageFraInternalDefects(SetWorkPackageFraInternalDefectsParameters parameters);
    Task<string> GetWorkPackageFraOtherInternalDefect(Guid applicationId);
    Task<GetWorkPackageFraFundingResult> GetWorkPackageFraFunding(Guid applicationId);
    Task SetWorkPackageFraFunding(SetWorkPackageFraFundingParameters parameters);
    Task<GetWorkPackageFraCheckYourAnswersResult> GetWorkPackageFraCheckYourAnswers(Guid applicationId);
    Task SetWorkPackageFraVisitedCheckYourAnswers(SetWorkPackageFraVisitedCheckYourAnswersParameters parameters);
    Task SetWorkPacakgeFraTaskStatus(SetWorkPacakgeFraTaskStatusParameters parameters);
    Task SubmitWorkPackageFra(Guid applciationId);
}