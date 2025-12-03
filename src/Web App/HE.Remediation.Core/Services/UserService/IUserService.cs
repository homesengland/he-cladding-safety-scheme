using HE.Remediation.Core.Services.UserService.Enum;
using HE.Remediation.Core.Services.UserService.Model;

namespace HE.Remediation.Core.Services.UserService;

public interface IUserService
{
    Task RecordSignIn(UserSignInModel signInModel);
    Task UpdateUserProfileCompletionStages(Guid userId, UserProfileCompletionModel userProfileCompletionModel);
    Task SetUserProfileStageCompletionStatus(EUserProfileStage profileStage, Guid userId, bool isComplete);
    Task<bool> IsUserDetailsDataCreated(string auth0UserId);
    Task<bool> IsUserProfileCompletionDataCreated(Guid userId);
    Task<bool> IsUserCompanyDetailsDataCreated(Guid userId);
    Task<bool> IsUserCompanyAddressDataCreated(Guid userId);
    Task<UserDetailsModel> GetUserDetailsByUserId(Guid userId);
    Task<UserDetailsModel> GetUserDetailsByAuth0UserId(string auth0UserId);
    Task<UserProfileCompletionModel> GetUserProfileCompletionData(Guid userId);
    Task<Guid> ScaffoldFirstTimeUserData(string auth0UserId, string emailAddress);
    Task ScaffoldUserProfileCompletionData(Guid userId);
    Task<UserInvitesPendingModel> IsUserInvitePending(string auth0UserId);
    Task<UserDetailsModel> GetUserDetailsByCompanyRegistrationNumber(string companyRegistrationNumber);
    Task<UserDetailsModel> GetUserDetailsByCompanyName(string companyName);
}