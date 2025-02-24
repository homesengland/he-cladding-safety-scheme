using HE.Remediation.Core.Interface;
using HE.Remediation.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace HE.Remediation.WebApp.Controllers
{
    public class CookiesController : Controller
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAnalyticsDataProivder _analyticsDataProivder;

        public CookiesController(IApplicationDataProvider applicationDataProvider, IHttpContextAccessor httpContextAccessor, 
            IAnalyticsDataProivder analyticsDataProivder)
        {
            _applicationDataProvider = applicationDataProvider;
            _httpContextAccessor = httpContextAccessor;
            _analyticsDataProivder = analyticsDataProivder;
        }

        public IActionResult Index()
        {
            if (HttpContext.Request.GetTypedHeaders().Referer != null)
            {
                ViewData["Referer"] = HttpContext.Request.GetTypedHeaders().Referer?.AbsolutePath;
            }

            ViewData["AppDataCookieName"] = _applicationDataProvider.GetCookieName;

            var model = GetCookiePolicy();

            return View(model);
        }

        [HttpPost(nameof(AcceptCookies))]
        public IActionResult AcceptCookies()
        {
            SetCookiePolicy(new CookiesAcceptedViewModel
            {
                AllowMeasureWebsite = true,
                AllowCommunicationsAndMarketing = true,
                AllowRememberYourSettings = true,
            });

            return RedirectToAction("Index", "LandingPage");
        }

        [HttpPost(nameof(RejectCookies))]
        public IActionResult RejectCookies()
        {
            SetCookiePolicy(new CookiesAcceptedViewModel
            {
                AllowMeasureWebsite = false,
                AllowCommunicationsAndMarketing = false,
                AllowRememberYourSettings = false,
            });

            return RedirectToAction("Index", "LandingPage");
        }

        [HttpPost(nameof(SetCookieSettings))]
        public IActionResult SetCookieSettings(CookiesAcceptedViewModel viewModel)
        {
            SetCookiePolicy(viewModel);

            return RedirectToAction("Index", "LandingPage");
        }

        private CookiesAcceptedViewModel GetCookiePolicy()
        {
            var cookiePolicy = _httpContextAccessor.HttpContext.Request.Cookies["cookies_policy"];
            var analyticsId = _analyticsDataProivder.GetAnalyticsId();

            if (cookiePolicy is null)
            {
                return new CookiesAcceptedViewModel
                {
                    AllowMeasureWebsite = false,
                    AllowCommunicationsAndMarketing = false,
                    AllowRememberYourSettings = false,
                    GoogleAnalyticsId = analyticsId.Replace("G-", "")
                };
            }

            var viewModel = JsonConvert.DeserializeObject<CookiesAcceptedViewModel>(cookiePolicy);
            viewModel.GoogleAnalyticsId = analyticsId?.Replace("G-", "");

            return viewModel;
        }

        private void SetCookiePolicy(CookiesAcceptedViewModel viewModel)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddYears(1),
                Path = "/"
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("cookies_preferences_set", "true", cookieOptions);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("cookies_policy", JsonConvert.SerializeObject(viewModel), cookieOptions);
        }
    }
}