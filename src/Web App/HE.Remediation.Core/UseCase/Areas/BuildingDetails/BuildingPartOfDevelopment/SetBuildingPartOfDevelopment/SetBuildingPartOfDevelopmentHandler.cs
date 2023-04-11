using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.SetBuildingPartOfDevelopment
{
    public class SetBuildingPartOfDevelopmentHandler : IRequestHandler<SetBuildingPartOfDevelopmentRequest>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetBuildingPartOfDevelopmentHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetBuildingPartOfDevelopmentRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateBuildingPartOfDevelopment(request, applicationId);

            return Unit.Value;
        }

        private async Task UpdateBuildingPartOfDevelopment(SetBuildingPartOfDevelopmentRequest request, Guid applicationId)
        {
            await _dbConnectionWrapper.ExecuteAsync("UpdateBuildingPartOfDevelopment",
                new
                {
                    applicationId,
                    request.PartOfDevelopment
                }
            );
        }
    }
}