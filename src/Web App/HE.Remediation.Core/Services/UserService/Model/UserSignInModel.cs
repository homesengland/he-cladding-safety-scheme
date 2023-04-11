namespace HE.Remediation.Core.Services.UserService.Model;

public record UserSignInModel
(
    Guid UserId,
    DateTime LoginTime,
    string IPAddress,
    string UserAgent
);