using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.GetPursuedSourcesFunding
{
    public class GetPursuedSourcesFundingHandler : IRequestHandler<GetPursuedSourcesFundingRequest, GetPursuedSourcesFundingResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IAlternateFundingRepository _alternateFundingRepository;

        public GetPursuedSourcesFundingHandler(
            IApplicationDataProvider applicationDataProvider,
            IAlternateFundingRepository alternateFundingRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _alternateFundingRepository = alternateFundingRepository;
        }

        public async ValueTask<GetPursuedSourcesFundingResponse> Handle(GetPursuedSourcesFundingRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var pursuedSourcesFunding = await _alternateFundingRepository.GetPursuedSourcesFunding(applicationId);

            var visitedCheckYourAnswers = await _alternateFundingRepository.GetAlternateFundingVisitedCheckYourAnswers(applicationId);

            return new GetPursuedSourcesFundingResponse
            {
                PursuedSourcesFunding = pursuedSourcesFunding,
                VisitedCheckYourAnswers = visitedCheckYourAnswers
            };
        }
    }
}
