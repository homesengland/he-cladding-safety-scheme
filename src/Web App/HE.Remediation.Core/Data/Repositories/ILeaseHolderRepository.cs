using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetCheckYourAnswers;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetCommunicationPartyDetails;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunication;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunicationType;

namespace HE.Remediation.Core.Data.Repositories;

public interface ILeaseHolderRepository
{
    Task<Guid> GetLeaseHolderEngagementIdForApplication(Guid applicationId);

    Task<GetResponsibleForCommunicationResponse> GetLeaseHolderResponsibleForCommunication(Guid leaseHolderEngagementId);

    Task UpdateLeaseHolderResponsibleForCommunication(UpdateLeaseHolderResponsibleForCommunicationParameters parameters);

    Task<GetResponsibleForCommunicationTypeResponse> GetLeaseHolderResponsibleForCommunicationType(Guid leaseHolderEngagementId);

    Task UpdateLeaseHolderResponsibleForCommunicationType(UpdateLeaseHolderResponsibleForCommunicationTypeParameters parameters);

    Task<IReadOnlyCollection<LeaseHolderEvidenceFile>> GetLeaseHolderEngagementFilesForApplication(Guid applicationId);

    Task<GetCommunicationPartyDetailsResponse> GetLeaseHolderCommunicationPartyDetails(Guid leaseHolderEngagementId);

    Task UpdateLeaseHolderCommunicationPartyDetails(UpdateLeaseHolderCommunicationPartyDetailsParameters parameters);

    Task<GetCheckYourAnswersResponse> GetLeaseHolderEngagementCheckYourAnswers(Guid applicationId);

}
