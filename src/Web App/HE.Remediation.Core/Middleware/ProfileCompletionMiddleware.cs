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

        var ignorePaths = new List<string>() 
        { 
            "/Administration", 
            "/Authentication/Logout",
            "/Accessibility",
            "/Cookies"
        };
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

        if (CheckPageAndPermission("/Administration/ContactInfoConsent",
                                   "/Administration/ContactDetails",
                                   userProfileCompletion.IsContactInformationComplete == false,
                                   context))
        {
            return true;
        }

        if (CheckPageAndPermission("/Administration/Profile",
                                   "/Administration/ContactInfoConsent",
                                   userProfileCompletion.IsContactConsentComplete == false,
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
                                   "/Administration/AddExtraContact",
                                   (userProfileCompletion.IsWantSecondaryContactComplete == false),
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
                                   "/Administration/AddExtraContact",
                                   userProfileCompletion.IsWantSecondaryContactComplete == false,
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
                if (!ProcessProfileStates(userProfileCompletion, context))
                {
                    context.Response.Redirect("/Administration/profile");
                    return true;
                }
            }

            return false;
        }
        else if (userProfileCompletion.ResponsibleEntityType == EResponsibleEntityType.Company)
        {
            // a company shouldn't go to a corresondence address
            var badPaths = new List<string>()
            {
                "/Administration/CorrespondenceAddress"                
            };
            if (badPaths.Any(segments => context.Request.Path.StartsWithSegments(segments,
                                                                                 StringComparison.OrdinalIgnoreCase)))
            {
                if (!ProcessProfileStates(userProfileCompletion, context))
                {
                    context.Response.Redirect("/Administration/profile");
                    return true;
                }                
            }

            return false;
        }
        return false;
    }

    private bool ProcessProfileStates(UserProfileCompletionModel userProfileCompletion, HttpContext context)
    {
        if (userProfileCompletion.IsContactInformationComplete == false)
        {
            // Redirect to Contact information (profile information) page
            context.Response.Redirect("/Administration/contactdetails");
            return true;
        }

        if (!userProfileCompletion.IsContactConsentComplete)
        {
            // Redirect to Contact information consent page
            context.Response.Redirect("/Administration/ContactInfoConsent");
            return true;
        }

        if (userProfileCompletion.IsResponsibleEntityTypeSelectionComplete == false)
        {
            // Redirect to Company or Individual (profile information) page
            context.Response.Redirect("/Administration/profile");
            return true;
        }

        if (userProfileCompletion.ResponsibleEntityType == EResponsibleEntityType.Company)
        {
            if (userProfileCompletion.IsCompanyDetailsComplete == false)
            {
                // Redirect to Company Details (profile information) page
                context.Response.Redirect("/Administration/companydetails");
                return true;
            }

            if (userProfileCompletion.IsCompanyAddressComplete == false)
            {
                // Redirect to Company Address (profile information) page
                context.Response.Redirect("/Administration/companyaddress");
                return true;
            }

            if (userProfileCompletion.IsSecondaryContactInformationComplete == false)
            {
                // Redirect to Secondary contact (profile information) page                
                context.Response.Redirect("/Administration/SecondaryContactDetails");
                return true;
            }

            // we have finished our profile so go to the settings page
            return false;
        }
        else if (userProfileCompletion.ResponsibleEntityType == EResponsibleEntityType.Individual)
        {
            if (userProfileCompletion.IsWantSecondaryContactComplete == false)
            {
                // Redirect to Contact information (profile information) page
                context.Response.Redirect("/Administration/AddExtraContact");
                return true;
            }

            if (userProfileCompletion.IsWantSecondaryContactComplete == false)
            {
                // Redirect to Contact information (profile information) page
                context.Response.Redirect("/Administration/AddExtraContact");
                return true;
            }

            if (userProfileCompletion.IsCorrespondenceAddressComplete == false)
            {
                // Redirect to Contact information (profile information) page
                context.Response.Redirect("/Administration/CorrespondenceAddress");
                return true;
            }

            // we have finished our profile so go to the settings page
            return false;
        }

        // if we are still here for an unknown profile page
        return false;
    }
}


