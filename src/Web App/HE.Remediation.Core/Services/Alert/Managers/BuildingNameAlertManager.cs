using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Services.Alert.Managers;

public abstract class BuildingNameAlertManager : IAlertManager
{
    protected readonly IBuildingDetailsRepository _buildingDetailsRepository;

    protected BuildingNameAlertManager(IBuildingDetailsRepository buildingDetailsRepository)
    {
        _buildingDetailsRepository = buildingDetailsRepository;
    }

    public abstract EAlertType AlertType { get; }
    public abstract Task<string> GetTitle(GetAlertsResult alert);

    protected async Task<string> GetBuildingName(Guid applicationId, bool withQuotes = true)
    {
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        return withQuotes 
            ? $"'{buildingName}'"
            : buildingName;
    }
}