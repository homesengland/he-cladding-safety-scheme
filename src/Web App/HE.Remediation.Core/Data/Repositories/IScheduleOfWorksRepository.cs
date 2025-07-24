using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.ScheduleOfWorks;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.Costs;
using HE.Remediation.Core.Data.StoredProcedureResults.ScheduleOfWorks;

namespace HE.Remediation.Core.Data.Repositories;

public interface IScheduleOfWorksRepository
{
    Task<bool> HasScheduleOfWorks();

    Task<bool> IsScheduleOfWorksApproved();

    Task<bool> IsScheduleOfWorksSubmitted();

    Task InsertScheduleOfWorks();

    Task ResetScheduleOfWorks();

    Task SubmitScheduleOfWorks(Guid? userId);

    Task<IReadOnlyCollection<FileResult>> GetContracts();
    Task<IReadOnlyCollection<FileResult>> GetScheduleOfWorksBuildingControlFiles(Guid applicationId);
    Task<IReadOnlyCollection<FileResult>> GetScheduleOfWorksLeaseholderEngagementFiles(Guid applicationId);

    Task<IReadOnlyCollection<string>> GetContractFileNames();

    Task InsertContract(Guid fileId);
    Task InsertBuildingControlFile(InsertBuildingControlFileParameters parameters);
    Task InsertLeaseholderEngagement(InsertLeaseholderEngagementParameters parameters);
    Task DeleteContract(Guid fileId);
    Task DeleteBuildingControlFile(DeleteBuildingControlFileParameters parameters);
    Task DeleteLeaseholderEngagement(DeleteLeaseholderEngagementParameters parameters);

    Task CreateCosts();

    Task<CostsResult> GetCosts();

    Task DeleteCosts();

    Task UpdateCosts(IEnumerable<UpdateCostParameters> parameters);

    Task<DeclarationResult> GetDeclaration();

    Task UpdateDeclaration(UpdateDeclarationParameters parameters);

    Task<ProjectDatesResult> GetProjectDates();

    Task UpdateProjectDates(UpdateProjectDatesParameters parameters);

    Task<AnswersResult> GetCheckYourAnswers();

    Task<IReadOnlyCollection<CostsProfileResult>> GetCostsProfile();

    Task<IReadOnlyCollection<CostsProfileResult>> GetApprovedApplicationScheduleOfWorksCosts();

    Task<OverviewResult> GetOverview();

    Task<OverviewResult> GetApprovedScheduleOfWorksOverview();
}
