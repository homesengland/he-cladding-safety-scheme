using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.GetCheckYourAnswers
{
    public class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly ILeaseHolderRepository _leaseHolderRepository;


        public GetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider, 
            IApplicationRepository applicationRepository,
            ILeaseHolderRepository leaseHolderRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _leaseHolderRepository = leaseHolderRepository;
        }

        public async ValueTask<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
        {
            return await GetCheckYourAnswers();
        }

        private async ValueTask<GetCheckYourAnswersResponse> GetCheckYourAnswers()
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationStatus = await _applicationRepository.GetApplicationStatus(applicationId);

            var leaseHolderEngagementId = await _leaseHolderRepository.GetLeaseHolderEngagementIdForApplication(applicationId);

            var responseCommunication = await _leaseHolderRepository.GetLeaseHolderResponsibleForCommunication(leaseHolderEngagementId);

            var answers = new GetCheckYourAnswersResponse();

            var leaseHolderEvidenceFiles = await _leaseHolderRepository.GetLeaseHolderEngagementFilesForApplication(applicationId);

            if (responseCommunication.ResponsibleForCommunication == ENoYes.No)
            {
                answers = await _leaseHolderRepository.GetLeaseHolderEngagementCheckYourAnswers(applicationId); 
            }
            answers.ResponsibleForCommunication = responseCommunication.ResponsibleForCommunication;
            answers.LeaseHolderEvidenceFiles = leaseHolderEvidenceFiles.ToList();
            answers.ReadOnly = applicationStatus.DeclarationConfirmed;
            return answers;
        }
    }
}
