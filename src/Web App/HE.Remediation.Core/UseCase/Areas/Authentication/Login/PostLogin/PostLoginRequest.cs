using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Authentication.Login.PostLogin;

public class PostLoginRequest : IRequest<PostLoginResponse>
{
    public string Auth0UserId { get; set; }
    public string EmailAddress { get; set; }
    public string IpAddress { get; set; }
    public string UserAgent { get; set; }
    public DateTime LoginDateTime { get; set; }
}