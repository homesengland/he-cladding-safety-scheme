using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.Middleware;

public static class ProfileCompletionMiddlewareExtensions
{
    public static IApplicationBuilder UseProfileCompletionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ProfileCompletionMiddleware>();
    }
}
