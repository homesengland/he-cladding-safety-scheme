using HE.Remediation.Core.Services.UserService.Model;

namespace HE.Remediation.Core.UseCase.Areas.Authentication.Login.PostLogin;

public class PostLoginResponse
{
    public UserProfileCompletionModel UserProfileCompletion { get; set; }
    public UserInvitesPendingModel UserInvitesPending { get; internal set; }
}