using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectSupport;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectSupport;

namespace HE.Remediation.Core.Data.Repositories;

public interface IProgressReportingProjectSupportRepository
{
    Task<GetProjectSupportDetailsResult> GetProjectSupportDetails(Guid applicationId, Guid projectReportId);
    Task SetProjectSupportDetails(SetProjectSupportParameters parameters);
    Task<GetProgressReportSupportTypeResult> GetProgressReportSupportType(GetProgressReportSupportTypeParameters parameters);
    Task SetProgressReportSupportType(SetProjectSupportTypeParameters parameters);
    Task<GetProjectSupportCheckYourAnswersResult> GetProjectSupportCheckYourAnswers(GetProjectSupportCheckYourAnswersParameters parameters);
    Task SetProjectSupportCheckYourAnswers(SetProjectSupportCheckYourAnswersParameters parameters);
}
