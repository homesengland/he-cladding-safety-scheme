using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.NameOfDevelopment.SetNameOfDevelopment
{
    public class SetNameOfDevelopmentHandler : IRequestHandler<SetNameOfDevelopmentRequest>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetNameOfDevelopmentHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<Unit> Handle(SetNameOfDevelopmentRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateNameOfDevelopment(request, applicationId);

            return Unit.Value;
        }

        private async Task UpdateNameOfDevelopment(SetNameOfDevelopmentRequest request, Guid applicationId)
        {
            await _dbConnectionWrapper.ExecuteAsync("UpdateNameOfDevelopment",
                new
                {
                    applicationId,
                    request.NameOfDevelopment
                }
            );
        }
    }
}
