using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Services.Alert.Managers;

public class ProgressReportAlertManager : BuildingNameAlertManager
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public ProgressReportAlertManager(IBuildingDetailsRepository buildingDetailsRepository, IProgressReportingRepository progressReportingRepository)
    : base(buildingDetailsRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public override EAlertType AlertType => EAlertType.ProgressReport;
    public override async Task<string> GetTitle(GetAlertsResult alert)
    {
        var report = await _progressReportingRepository.GetLatestProgressReport(alert.ApplicationId);
        var buildingName = await GetBuildingName(alert.ApplicationId);
        return $"{report?.DateCreated ?? alert.CreationDate:MMMM} Progress Report for {buildingName} now available to complete";
    }
}