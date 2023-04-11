using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.SupportRequired.SetSupportRequired
{
    public class SetSupportRequiredHandler : IRequestHandler<SetSupportRequiredRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetSupportRequiredHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetSupportRequiredRequest request, CancellationToken cancellationToken)
        {
            await UpsertSupportRequired(request);
            return Unit.Value;
        }

        private async Task UpsertSupportRequired(SetSupportRequiredRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _dbConnectionWrapper.ExecuteAsync("UpsertPreTenderSupportRequired", new { applicationId, request.SupportRequired });
        }
    }
}
