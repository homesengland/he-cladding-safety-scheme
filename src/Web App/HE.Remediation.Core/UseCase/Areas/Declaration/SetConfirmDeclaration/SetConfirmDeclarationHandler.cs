using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Declaration.SetConfirmDeclaration
{
    public class SetConfirmDeclarationHandler : IRequestHandler<SetConfirmDeclarationRequest, SetConfirmDeclarationResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;

        public SetConfirmDeclarationHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider, IApplicationRepository applicationRepository)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
        }

        public async Task<SetConfirmDeclarationResponse> Handle(SetConfirmDeclarationRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            var canSubmit = await CanSubmit(applicationId);
            if (canSubmit)
            {
                await UpdateConfirmDeclarationRequest(applicationId);
            }

            return new SetConfirmDeclarationResponse
            {
                Success = canSubmit,
                ErrorMessage = canSubmit ? string.Empty : "Cannot submit declaration because the building is ineligible for grant funding. Your application has been closed."
            };
        }

        private async Task<Unit> UpdateConfirmDeclarationRequest(Guid applicationId)
        {
            
            await _dbConnectionWrapper.ExecuteAsync("UpdateConfirmDeclaration", new { applicationId });

            await _applicationRepository.UpdateInternalStatus(applicationId, EApplicationInternalStatus.InitialApplicationSubmitted);

            return Unit.Value;
        }

        private async Task<bool> CanSubmit(Guid applicationId)
        {
            var statusResult = await _applicationRepository.GetApplicationStatus(applicationId);
            return statusResult.Stage != EApplicationStage.Closed;
        }

    }
}
