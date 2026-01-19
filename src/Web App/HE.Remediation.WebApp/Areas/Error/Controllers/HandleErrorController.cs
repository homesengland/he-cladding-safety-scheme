using HE.Remediation.Core.Interface;
using Microsoft.AspNetCore.Mvc;
namespace HE.Remediation.WebApp.Areas.Error.Controllers
{
    [Area("Error")]
    public class HandleErrorController : Controller
    {
        private readonly ILogger<HandleErrorController> _logger;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public HandleErrorController(ILogger<HandleErrorController> logger, IApplicationDataProvider applicationDataProvider)
        {
            _logger = logger;
            _applicationDataProvider = applicationDataProvider;
        }

        [Route("/Error/HandleError/{errorStatusCode:int}")]
        public IActionResult Index(int errorStatusCode)
        {
            var auth0UserId = _applicationDataProvider.GetAuth0UserId();

            if (errorStatusCode == 503)
            {
                var requestedUrl = HttpContext.Request.Path + HttpContext.Request.QueryString;
                var headers = string.Join(", ", HttpContext.Request.Headers.Select(h => $"{h.Key}: {h.Value}"))
                    .Replace("\r", "")
                    .Replace("\n", "");
                var user = HttpContext.User?.Identity?.Name ?? "Anonymous";
                _logger.LogError("503 Service Unavailable: {Url} | User: {User} | Headers: {Headers} | Time: {Time}",
                    requestedUrl, user, headers, DateTime.UtcNow);
            }

            return errorStatusCode switch
            {
                403 => View("Forbidden"),
                404 => View("NotFound"),
                408 => View("SessionTimeout"),
                503 => View("ServiceUnavailable"),
                _ => View("InternalServerError")
            };
        }
    }
}
