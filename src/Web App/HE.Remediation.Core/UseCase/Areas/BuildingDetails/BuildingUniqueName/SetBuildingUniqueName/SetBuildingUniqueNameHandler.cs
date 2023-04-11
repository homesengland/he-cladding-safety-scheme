using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingUniqueName.GetBuildingUniqueName;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingUniqueName.SetBuildingUniqueName
{
    public class SetBuildingUniqueNameHandler : IRequestHandler<SetBuildingUniqueNameRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetBuildingUniqueNameHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetBuildingUniqueNameRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetBuildingUniqueNameResponse>("GetBuildingUniqueName", new { applicationId });

            if (response is not null)
            {
                await UpdateUniqueBuildingName(request, applicationId);
                return Unit.Value;
            }

            await InsertUniqueBuildingName(request, applicationId);
            return Unit.Value;
        }

        private async Task InsertUniqueBuildingName(SetBuildingUniqueNameRequest request, Guid applicationId)
        {
            var buildingDetailsId = Guid.NewGuid();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _dbConnectionWrapper.ExecuteAsync("InsertBuildingUniqueName", new { buildingDetailsId, request.UniqueName });

                await _dbConnectionWrapper.ExecuteAsync("UpdateBuildingDetailsId", new { applicationId, buildingDetailsId });

                scope.Complete();
            }
        }

        private async Task UpdateUniqueBuildingName(SetBuildingUniqueNameRequest request, Guid applicationId)
        {
            await _dbConnectionWrapper.ExecuteAsync("UpdateBuildingUniqueName", new { applicationId, request.UniqueName });
        }
    }
}
