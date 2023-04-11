﻿using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.Error.Controllers
{
    [Area("Error")]
    public class HandleErrorController : Controller
    {
        [Route("/Error/HandleError/{errorStatusCode:int}")]
        public async Task<IActionResult> Index(int errorStatusCode)
        {
            return errorStatusCode switch
                {
                    404 => View("NotFound"),
                    408 => View("SessionTimeout"),
                    503 => View("ServiceUnavailable"),
                    _ => View("InternalServerError")
                };
        }
    }
}
