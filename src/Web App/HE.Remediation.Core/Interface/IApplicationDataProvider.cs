using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Services.UserService.Enum;
using HE.Remediation.Core.Services.UserService.Model;

namespace HE.Remediation.Core.Interface
{
    public interface IApplicationDataProvider
    {
        void SetApplicationId(Guid applicationId);
        Guid GetApplicationId();
        void SetUserId(Guid userId);
        Guid? GetUserId();
        void SetAuth0UserId(string auth0UserId);
        string GetAuth0UserId();

        void SetUserDetails(Guid userId, string auth0UserId, UserProfileCompletionModel profileCompletionModel);

        EResponsibleEntityType? GetResponsibleEntityType();
        void SetResponsibleEntityType(EResponsibleEntityType entityType);

        UserProfileCompletionModel GetProfileCompletion();

        void SetProfileCompletion(UserProfileCompletionModel profileCompletion);

        void SetUserProfileStageCompletionStatus(EUserProfileStage profileStage);

        void SetUserProfileStageCompletionStatus(EUserProfileStage profileStage, EResponsibleEntityType entityType);        
        
        DateTimeOffset? GetSessionTimeout();
        void SetSessionTimeout();
        string GetCookieName
        {
            get;
        }

        bool IsEnforcedFlow();

        void SetProgressReportId(Guid progressReportId);       

        Guid GetProgressReportId();

        void SetPaymentRequestId(Guid paymentRequestId);

        Guid GetPaymentRequestId();

        string GetApplicationEmailAddress();

        void SetApplicationIdAndEmailAddress(Guid applicationId, string emailAddress);
    }
}
