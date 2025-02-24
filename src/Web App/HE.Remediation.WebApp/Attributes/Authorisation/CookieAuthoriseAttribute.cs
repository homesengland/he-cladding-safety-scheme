using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Attributes.Authorisation;

public class CookieAuthoriseAttribute : TypeFilterAttribute
{
    public CookieAuthoriseAttribute() : base(typeof(CookieAuthoriseFilter))
    {
    }
}