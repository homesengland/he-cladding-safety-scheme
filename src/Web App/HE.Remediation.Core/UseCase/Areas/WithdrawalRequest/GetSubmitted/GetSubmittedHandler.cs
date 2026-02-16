using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.GetSubmitted
{
    public class GetSubmittedHandler : IRequestHandler<GetSubmittedRequest, GetSubmittedResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;

        public GetSubmittedHandler(
            IApplicationDataProvider applicationDataProvider,
            IApplicationRepository applicationRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
        }

        public async ValueTask<GetSubmittedResponse> Handle(GetSubmittedRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);

            return new GetSubmittedResponse
            {
                ApplicationReferenceNumber = applicationReferenceNumber
            };
        }
    }
}
