using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Services.StatusTransition;

public interface IStatusTransitionService
{
    Task TransitionToStatus(EApplicationStage stage, EApplicationStatus status, string reason = null, params Guid[] applicationIds);
    Task TransitionToStatus(EApplicationStatus status, string reason = null, params Guid[] applicationIds);
    Task TransitionToInternalStatus(EApplicationInternalStatus internalStatus, string reason = null, params Guid[] applicationIds);
}