using HE.Remediation.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HE.Remediation.Core.Attributes
{
    public class UserIdentityMustBeTheApplicationUserAttribute : TypeFilterAttribute
    {
        public UserIdentityMustBeTheApplicationUserAttribute() : base(typeof(ApplicationUserIdentityAttribute))
        {
        }

        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
        public class ApplicationUserIdentityAttribute : Attribute, IAuthorizationFilter
        {
            private readonly IApplicationDataProvider _applicationDataProvider;

            public ApplicationUserIdentityAttribute(IApplicationDataProvider applicationDataProvider)
            {
                _applicationDataProvider = applicationDataProvider;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                if (_applicationDataProvider.GetApplicationEmailAddress() != context.HttpContext.User.Identity.Name)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
}
