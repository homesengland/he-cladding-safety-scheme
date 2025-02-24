using HE.Remediation.Core.UseCase.Areas.BuildingDetails.LocalAuthority.GetLocalAuthorityCostCentre;

namespace HE.Remediation.Core.Data.Repositories
{
    public interface ILocalAuthorityCostCentreRepository
    {
        Task<IReadOnlyCollection<LocalAuthorityCostCentre>> GetCostCentres();
    }
}