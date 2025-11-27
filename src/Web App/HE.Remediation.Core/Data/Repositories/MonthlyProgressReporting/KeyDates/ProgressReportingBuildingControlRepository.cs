using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.BuildingControl;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates.BuildingControl;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
public class ProgressReportingBuildingControlRepository : IProgressReportingBuildingControlRepository
{
    private readonly IDbConnectionWrapper _connection;
    public ProgressReportingBuildingControlRepository(
        IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task DeleteUploadBuildingControlFile(DeleteUploadBuildingControlParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(DeleteUploadBuildingControlFile), parameters);
    }

    public async Task<GetUploadBuildingControlDocumentsResult> GetUploadBuildingControlDocuments(GetUploadBuildingControlDocumentsParameters parameters)
    {
        GetUploadBuildingControlDocumentsResult result = null;

        await _connection.QueryAsync<GetUploadBuildingControlDocumentsResult, FileResult, GetUploadBuildingControlDocumentsResult>(
            nameof(GetUploadBuildingControlDocuments),
            (buildingControl, file) =>
            {
                result ??= buildingControl;

                if (file is not null && result.BuildingControlDocuments.All(x => x.Id != file.Id))
                {
                    result.BuildingControlDocuments.Add(file);
                }

                return result;
            },
            parameters);
        return result;
    }

    public async Task InsertProgressReportBuildingControlFile(InsertProgressReportProjectPlanFileParameters parameters)
    {
        await _connection.ExecuteAsync("InsertProgressReportBuildingControlFile", parameters);
    }
}
