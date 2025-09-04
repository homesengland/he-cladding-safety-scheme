using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.Repositories;

public interface IAlternateFundingRepository
{
    Task<ECostRecoveryType?> GetCostRecoveryType(Guid applicationId);
    Task SetCostRecoveryType(SetCostRecoveryTypeParameters parameters);
    Task<IReadOnlyCollection<EPartyPursuedRole>> GetPartyPursuedRoles(Guid applciationId);
    Task SetPartyPursedRoles(SetPartyPursedRolesParameters parameters);
    Task<string> GetOtherPartyPursuedRole(Guid applciationId);
    Task SetOtherPartyPursuedRole(SetOtherPartyPursuedRoleParameters parameters);
    Task<GetFundingRoutesCheckYourAnswersResult> GetFundingRoutesCheckYourAnswers(Guid applicationId);
    Task<bool> GetAlternateFundingVisitedCheckYourAnswers(Guid applicationId);
    Task SetAlternateFundingVisitedCheckYourAnswers(SetAlternateFundingVisitedCheckYourAnswersParameters parameters);
    Task<EPursuedSourcesFundingType?> GetPursuedSourcesFunding(Guid applicationId);
}