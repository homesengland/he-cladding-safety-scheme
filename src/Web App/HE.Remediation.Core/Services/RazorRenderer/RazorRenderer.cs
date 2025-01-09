using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace HE.Remediation.Core.Services.RazorRenderer
{
    public class RazorRenderer : IRazorRenderer
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public RazorRenderer(IHttpContextAccessor httpContextAccessor, IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        public async Task<string> RenderView(string templatePath, object model)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                var requestServices = _serviceProvider.CreateScope();
                httpContext = new DefaultHttpContext { RequestServices = requestServices.ServiceProvider };
            }

            var viewResult = _razorViewEngine.GetView(null, templatePath, false);

            if (viewResult.View == null)
                throw new ArgumentNullException(nameof(viewResult.View), $"Unable to find the template at path {templatePath}");

            var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };

            var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new ActionDescriptor());

            var stringWriter = new StringWriter();

            var viewContext = new ViewContext(
                actionContext,
                viewResult.View,
                viewDictionary,
                new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                stringWriter,
                new HtmlHelperOptions());

            await viewResult.View.RenderAsync(viewContext);

            return stringWriter.ToString();
        }
    }
}
