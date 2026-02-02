using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.SupportRequired.SetSupportRequired
{
    public class SetSupportRequiredHandler : IRequestHandler<SetSupportRequiredRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IPreTenderRepository _preTenderRepository;

        public SetSupportRequiredHandler(
            IDbConnectionWrapper dbConnectionWrapper, 
            IApplicationDataProvider applicationDataProvider, 
            IPreTenderRepository preTenderRepository)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _preTenderRepository = preTenderRepository;
        }

        public async ValueTask<Unit> Handle(SetSupportRequiredRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            if (await _preTenderRepository.IsPreTenderSubmitted(applicationId))
            {
                return Unit.Value;
            }

            await _dbConnectionWrapper.ExecuteAsync("UpsertPreTenderSupportRequired", new { applicationId, request.SupportRequired });
            return Unit.Value;
        }
    }
}
