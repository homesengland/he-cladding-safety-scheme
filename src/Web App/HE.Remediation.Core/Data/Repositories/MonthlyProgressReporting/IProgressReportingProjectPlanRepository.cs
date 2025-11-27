using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectPlan;

namespace HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;

public interface IProgressReportingProjectPlanRepository
{
    Task InsertPtsUpliftDocument(InsertPtsUpliftDocumentParameters parameters);
    Task<IReadOnlyCollection<FileResult>> GetPtsUpliftDocument(GetPtsUpliftDocumentParameters parameters);
    Task DeletePtsUpliftDocument(DeletePtsUpliftDocumentParameters parameters);
    Task<GetProjectPlanCheckYourAnswersResult> GetProjectPlanCheckYourAnswers(GetProjectPlanCheckYourAnswersParameters parameters);
    Task SetProjectPlanTaskStatus(SetProjectPlanTaskStatusParameters parameters);
    Task DeleteMonthlyProgressReportUploadProjectPlanFile(MonthlyProgressReportDeleteUploadProjectPlanParameters parameters);
    Task<GetProjectPlanDetailsResult> GetProjectPlanDetails(Guid applicationId, Guid projectReportId);
    Task<GetMonthlyReportProjectPlanDocumentsResult> GetMonthlyReportProjectPlanDocuments(GetMonthlyReportProjectPlanDocumentsParameters parameters);
    Task SetMonthlyProgressReportProjectPlanCheckYourAnswers(SetMonthlyProgressReportProjectPlanCheckYourAnswersParameters parameters);
    Task SetProjectPlanDetails(SetProjectPlanParameters parameters);
    Task InsertProgressReportProjectPlanFile(InsertProgressReportProjectPlanFileParameters parameters);
}