namespace HE.Remediation.Core.Services.RazorRenderer
{
    public interface IRazorRenderer
    {
        Task<string> RenderView(string templatePath, object model);
    }
}
