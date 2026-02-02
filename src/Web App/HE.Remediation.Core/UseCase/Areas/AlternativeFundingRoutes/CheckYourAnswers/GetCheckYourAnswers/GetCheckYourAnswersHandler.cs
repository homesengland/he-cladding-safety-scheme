using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.CheckYourAnswers.GetCheckYourAnswers
{
    public class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IAlternateFundingRepository _alternateFundingRepository;
        private readonly IApplicationRepository _applicationRepository;

        public GetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider, IAlternateFundingRepository alternateFundingRepository, IApplicationRepository applicationRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _alternateFundingRepository = alternateFundingRepository;
            _applicationRepository = applicationRepository;
        }

        public async ValueTask<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationStatus = await _applicationRepository.GetApplicationStatus(applicationId);

            var answers = await _alternateFundingRepository.GetFundingRoutesCheckYourAnswers(applicationId);

            var isSocialSector = await _applicationRepository.IsSocialSector(applicationId);
            var declarationConfirmed = applicationStatus.DeclarationConfirmed;

            await _alternateFundingRepository.SetAlternateFundingVisitedCheckYourAnswers(
                new SetAlternateFundingVisitedCheckYourAnswersParameters
                {
                    ApplicationId = applicationId,
                    VisitedCheckYourAnswers = true
                });

            return new GetCheckYourAnswersResponse
            {
                CostRecoveryType = answers.CostRecoveryType,
                FundingRouteTypes = answers.FundingRouteTypes.Select(x => x.Type).ToList(),
                OtherPartyPursuedRole = answers.OtherPartyPursuedRole,
                PartyPursuedRoles = answers.PartyPursuedRoles.ToList(),
                OtherSourcesPursuedType = answers.OtherSourcesPursuedType,
                OtherSourcesPursuedTypeId = answers.OtherSourcesPursuedTypeId,
                IsSocialSector = isSocialSector,
                ReadOnly = declarationConfirmed
            };
        }
    }
}
