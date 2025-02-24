namespace HE.Remediation.Core.Services.PdfRendererService
{
    public interface IPdfRenderer
    {
        Task<byte[]> RenderPdf<T>(string templatePath, T model, PdfRenderOptions pdfRenderOptions = null);

        Task<byte[]> RenderGotNotifyLetterPdf<T>(string templatePath, T model);
    }
}
