using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.Submit.SetSubmit
{
    public class SetSubmitHandler : IRequestHandler<SetSubmitRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetSubmitHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetSubmitRequest request, CancellationToken cancellationToken)
        {
            await UpdateSubmitRequest();
            return Unit.Value;
        }

        private async Task<Unit> UpdateSubmitRequest()
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _dbConnectionWrapper.ExecuteAsync("UpdateApplicationSubmit", new { applicationId });

            return Unit.Value;
        }
    }
}
