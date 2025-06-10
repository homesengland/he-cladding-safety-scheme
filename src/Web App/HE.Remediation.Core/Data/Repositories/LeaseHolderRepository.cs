using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetCommunicationPartyDetails;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunication;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunicationType;

using static System.Net.Mime.MediaTypeNames;

namespace HE.Remediation.Core.Data.Repositories
{
    public class LeaseHolderRepository : ILeaseHolderRepository
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;

        public LeaseHolderRepository(IDbConnectionWrapper dbConnectionWrapper)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
        }

        public async Task<Guid> GetLeaseHolderEngagementIdForApplication(Guid applicationId)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<Guid>("GetLeaseHolderEngagementIdForApplication",
                         new
                         {
                             ApplicationId = applicationId
                         });

            return result;
        }

        public async Task<GetResponsibleForCommunicationResponse> GetLeaseHolderResponsibleForCommunication(Guid leaseHolderEngagementId)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetResponsibleForCommunicationResponse>("GetLeaseHolderResponsibleForCommunication",
                                                  new
                                                  {
                                                      LeaseHolderEngagementId = leaseHolderEngagementId
                                                  });

            return result;
        }

        public async Task UpdateLeaseHolderResponsibleForCommunication(UpdateLeaseHolderResponsibleForCommunicationParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync("UpdateLeaseHolderResponsibleForCommunication", new
            {
                LeaseHolderEngagementId = parameters.LeaseHolderEngagementId,
                ResponsibleForCommunication = parameters.ResponsibleForCommunication
            });
        }

        public async Task<GetCommunicationPartyDetailsResponse> GetLeaseHolderCommunicationPartyDetails(Guid leaseHolderEngagementId)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetCommunicationPartyDetailsResponse>("GetApplicationLeaseHolderCommunicationPartyDetails",
                                                 new
                                                 {
                                                     ApplicationLeaseHolderEngagementId = leaseHolderEngagementId
                                                 });

            return result;
        }

        public async Task<GetCheckYourAnswersResponse> GetLeaseHolderEngagementCheckYourAnswers(Guid applicationId)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetCheckYourAnswersResponse>("GetLeaseHolderEngagementCheckYourAnswers", new
            {
                ApplicationId = applicationId
            });

            return result;
        }

        public async Task<IReadOnlyCollection<LeaseHolderEvidenceFile>> GetLeaseHolderEngagementFilesForApplication(Guid applicationId)
        {
            var result = await _dbConnectionWrapper.QueryAsync<LeaseHolderEvidenceFile>("GetLeaseHolderEngagementFilesForApplication", new
            {
                ApplicationId = applicationId
            });

            return result;
        }

        public async Task UpdateLeaseHolderCommunicationPartyDetails(UpdateLeaseHolderCommunicationPartyDetailsParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync("UpdateApplicationLeaseHolderCommunicationPartyDetails", new
            {
                ContactName = parameters.ContactName,
                CompanyName = parameters.CompanyName,
                EmailAddress = parameters.EmailAddress,
                ContactNumber = parameters.ContactNumber,
                ApplicationLeaseHolderEngagementId = parameters.ApplicationLeaseHolderEngagementId,
            });
        }

        public async Task<GetResponsibleForCommunicationTypeResponse> GetLeaseHolderResponsibleForCommunicationType(Guid leaseHolderEngagementId)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetResponsibleForCommunicationTypeResponse>("GetLeaseHolderResponsibleForCommunicationType",
                                                 new
                                                 {
                                                     LeaseHolderEngagementId = leaseHolderEngagementId
                                                 });

            return result;
        }

        public async Task UpdateLeaseHolderResponsibleForCommunicationType(UpdateLeaseHolderResponsibleForCommunicationTypeParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync("UpdateLeaseHolderResponsibleForCommunicationType", new
            {
                LeaseHolderEngagementId = parameters.LeaseHolderEngagementId,
                ResponsibleForCommunicationTypeId = (int)parameters.ResponsibleForCommunicationTypeId,
                RepresentationOtherText = parameters.RepresentationOtherText
            });
        }
    }
}
