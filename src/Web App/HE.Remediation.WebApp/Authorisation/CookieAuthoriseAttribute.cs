using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Authorisation;

public class CookieAuthoriseAttribute : TypeFilterAttribute
{
    public CookieAuthoriseAttribute() : base(typeof(CookieAuthoriseFilter))
    {
    }
}