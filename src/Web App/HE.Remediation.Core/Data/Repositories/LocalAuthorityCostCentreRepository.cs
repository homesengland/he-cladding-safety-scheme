using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.LocalAuthority.GetLocalAuthorityCostCentre;

namespace HE.Remediation.Core.Data.Repositories
{
    public class LocalAuthorityCostCentreRepository : ILocalAuthorityCostCentreRepository
    {
        private readonly IDbConnectionWrapper _connection;
        public LocalAuthorityCostCentreRepository(IDbConnectionWrapper connection)
        {
            _connection = connection;
        }
        public async Task<IReadOnlyCollection<LocalAuthorityCostCentre>> GetCostCentres()
        {
            var results = await _connection.QueryAsync<LocalAuthorityCostCentre>(nameof(GetCostCentres));
            return results;
        }
    }
}
