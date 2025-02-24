using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Attributes.Authorisation;

public class CookieApplicationAuthoriseAttribute : TypeFilterAttribute
{
    public CookieApplicationAuthoriseAttribute() : base(typeof(CookieApplicationAuthoriseFilter))
    {
    }
}