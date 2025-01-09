using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResetSection
{
    public class ResetBuildingDetailsSectionHandler : IRequestHandler<ResetBuildingDetailsSectionRequest>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;


        public ResetBuildingDetailsSectionHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(ResetBuildingDetailsSectionRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _dbConnectionWrapper.ExecuteAsync("ResetBuildingDetailsSection", new { applicationId });

            return Unit.Value;
        }
    }

    public class ResetBuildingDetailsSectionRequest : IRequest 
    { 
    }
}
