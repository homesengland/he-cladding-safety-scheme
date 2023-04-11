using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Declaration.SetConfirmDeclaration
{
    public class SetConfirmDeclarationHandler : IRequestHandler<SetConfirmDeclarationRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetConfirmDeclarationHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetConfirmDeclarationRequest request, CancellationToken cancellationToken)
        {
            await UpdateConfirmDeclarationRequest();
            return Unit.Value;
        }

        private async Task<Unit> UpdateConfirmDeclarationRequest()
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _dbConnectionWrapper.ExecuteAsync("UpdateConfirmDeclaration", new { applicationId });

            return Unit.Value;
        }
    }
}
