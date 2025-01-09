using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.SetCladdingArea
{
    public class SetCladdingAreaHandler : IRequestHandler<SetCladdingAreaRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetCladdingAreaHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetCladdingAreaRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            await _dbConnectionWrapper
                .ExecuteAsync("UpdateRecommendedCladdingArea", new
                {
                    ApplicationId = applicationId,
                    request.RecommendedCladdingArea
                });
            return Unit.Value;
        }
    }
}
