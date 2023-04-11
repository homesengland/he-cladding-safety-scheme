using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Authorisation;

public class CookieApplicationAuthoriseAttribute : TypeFilterAttribute
{
    public CookieApplicationAuthoriseAttribute() : base(typeof(CookieApplicationAuthoriseFilter))
    {
    }
}