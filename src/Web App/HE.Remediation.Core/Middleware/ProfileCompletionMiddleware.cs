using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HE.Remediation.Core.Middleware;

public class ProfileCompletionMiddleware
{
    private readonly RequestDelegate _next;
    private const string CookieName = "AppData";

    public ProfileCompletionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity is not { IsAuthenticated: true })
        {
            await _next(context);
            return;
        }
        var applicationDataProvider = context.RequestServices.GetRequiredService<IApplicationDataProvider>();
        if (applicationDataProvider == null)
        {
            await _next(context);
            return;
        }
        var userId = applicationDataProvider.GetUserId();
        if (userId == null)
        {
            await _next(context);
            return;
        }

        var userProfileCompletion = applicationDataProvider.GetProfileCompletion();
        if (!RedirectToProfilePageIfRequired(userProfileCompletion, context))
        {
            await _next(context);
        }
    }

    private bool RedirectToProfilePageIfRequired(UserProfileCompletionModel userProfileCompletion, HttpContext context)
    {
        if (PreventUserForwardSkipping(userProfileCompletion, context))
        {
            return false;
        }

        if (HandleSpecificPages(userProfileCompletion, context))
        {
            return false;
        }

        var ignorePaths = new List<string>() { "/Administration", "/Authentication/Logout" };
        if (ignorePaths.Any(segments => context.Request.Path.StartsWithSegments(segments,
                                                                                StringComparison.OrdinalIgnoreCase)))
        {
            return false;
        }

        return ProcessProfileStates(userProfileCompletion, context);
    }

    private bool PreventUserForwardSkipping(UserProfileCompletionModel userProfileCompletion, HttpContext context)
    {
        // if we don't have a user completion, just continue
        if (userProfileCompletion == null)
        {
            return false;
        }

        if (CheckPageAndPermission("/Administration/CorrespondanceAddress",
                                   "/Administration/ContactDetails",
                                   !userProfileCompletion.IsContactInformationComplete,
                                   context))
        {
            return true;
        }

        if (CheckPageAndPermission("/Administration/Profile",
                                   "/Administration/CorrespondanceAddress",
                                   !userProfileCompletion.IsCorrespondenceAddressComplete,
                                   context))
        {
            return true;
        }

        if (userProfileCompletion.ResponsibleEntityType == EResponsibleEntityType.Company)
        {
            if (CheckPageAndPermission("/Administration/CompanyAddress",
                                       "/Administration/CompanyDetails",
                                       (userProfileCompletion.IsCompanyDetailsComplete == false),
                                       context))
            {
                return true;
            }

            if (CheckPageAndPermission("/Administration/SecondaryContactDetails",
                                   "/Administration/CompanyAddress",
                                   (userProfileCompletion.IsCompanyAddressComplete == false),
                                   context))
            {
                return true;
            }
        }
        else if (userProfileCompletion.ResponsibleEntityType == EResponsibleEntityType.Individual)
        {
            if (CheckPageAndPermission("/Administration/SecondaryContactDetails",
                                   "/Administration/CorrespondanceAddress",
                                   (userProfileCompletion.IsCorrespondenceAddressComplete == false),
                                   context))
            {
                return true;
            }
        }
        else
        {
            if (CheckPageAndPermission("/Administration/CompanyDetails",
                                       "/Administration/ContactDetails",
                                       true,
                                       context))
            {
                return true;
            }

            if (CheckPageAndPermission("/Administration/CompanyAddress",
                                       "/Administration/ContactDetails",
                                       true,
                                       context))
            {
                return true;
            }

            if (CheckPageAndPermission("/Administration/SecondaryContactDetails",
                                       "/Administration/ContactDetails",
                                       true,
                                       context))
            {
                return true;
            }
        }


        return false;
    }

    private bool CheckPageAndPermission(string searchPage, string targetPage, bool shouldRedirect, HttpContext context)
    {
        var searchPaths = new List<string>()
        {
            searchPage
        };
        if (searchPaths.Any(segments => context.Request.Path.StartsWithSegments(segments,
                                                                                StringComparison.OrdinalIgnoreCase)))
        {
            if (shouldRedirect)
            {
                context.Response.Redirect(targetPage);
                return true;
            }
        }
        return false;
    }

    private bool HandleSpecificPages(UserProfileCompletionModel userProfileCompletion, HttpContext context)
    {
        if (userProfileCompletion.ResponsibleEntityType == EResponsibleEntityType.Individual)
        {
            // we do not want to go to any company related URL's
            var badPaths = new List<string>()
            {
                "/Administration/companydetails",
                "/Administration/companyaddress"
            };
            if (badPaths.Any(segments => context.Request.Path.StartsWithSegments(segments,
                                                                                 StringComparison.OrdinalIgnoreCase)))
            {
                context.Response.Redirect("/Administration/profile");
                return true;
            }

            return false;
        }
        return false;
    }

    private bool ProcessProfileStates(UserProfileCompletionModel userProfileCompletion, HttpContext context)
    {
        if (userProfileCompletion.IsContactInformationComplete == false)
        {
            // Replace with redirection to Contact information (profile information) page
            context.Response.Redirect("/Administration/contactdetails");
            return true;
        }

        if (userProfileCompletion.IsCorrespondenceAddressComplete == false)
        {
            // Replace with redirection to Contact information (profile information) page
            context.Response.Redirect("/Administration/CorrespondanceAddress");
            return true;
        }

        if (userProfileCompletion.IsResponsibleEntityTypeSelectionComplete == false)
        {
            // TODO: Replace with redirection to Company or Individual (profile information) page
            context.Response.Redirect("/Administration/profile");
            return true;
        }
        if (userProfileCompletion.ResponsibleEntityType == EResponsibleEntityType.Company)
        {
            if (userProfileCompletion.IsCompanyDetailsComplete == false)
            {
                // TODO: Replace with redirection to Company Details (profile information) page
                context.Response.Redirect("/Administration/companydetails");
                return true;
            }

            if (userProfileCompletion.IsCompanyAddressComplete == false)
            {
                // TODO: Replace with redirection to Company Address (profile information) page
                context.Response.Redirect("/Administration/companyaddress");
                return true;
            }
        }
        if (userProfileCompletion.IsSecondaryContactInformationComplete == false)
        {
            // TODO: Replace with redirection to Secondary contact (profile information) page                
            context.Response.Redirect("/Administration/SecondaryContactDetails");
            return true;
        }

        return false;
    }
}


