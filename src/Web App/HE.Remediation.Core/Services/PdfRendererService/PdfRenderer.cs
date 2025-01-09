using HE.Remediation.Core.Services.RazorRenderer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;

namespace HE.Remediation.Core.Services.PdfRendererService
{
    public class PdfRenderer : IPdfRenderer
    {
        private readonly IRazorRenderer _razorRenderer;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<PdfRenderer> _logger;

        public PdfRenderer(IRazorRenderer razorRenderer, IWebHostEnvironment webHostEnvironment, ILogger<PdfRenderer> logger)
        {
            _razorRenderer = razorRenderer;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<byte[]> RenderPdf<T>(string templatePath, T model, PdfRenderOptions pdfRenderOptions = null)
        {
            var html = await _razorRenderer.RenderView(templatePath, model);

            _logger.LogDebug("Razor Renderer output: {html}", html);

            return TransformToPdf(html, pdfRenderOptions);
        }

        private byte[] TransformToPdf(string html, PdfRenderOptions pdfRenderOptions)
        {
            try
            {
                var baseUrl = _webHostEnvironment.WebRootPath;

                _logger.LogDebug("Base Url {baseUrl}", baseUrl);

                var htmlConverter = new HtmlToPdfConverter();

                var settings = new BlinkConverterSettings
                {
                    Orientation = pdfRenderOptions?.Orientation ?? PdfPageOrientation.Portrait,
                    MediaType = pdfRenderOptions?.MediaType ?? MediaType.Print,
                    Margin = pdfRenderOptions?.Margins ?? new PdfMargins
                    {
                        All = 0f
                    }
                };

                _logger.LogDebug("PDF Orientation {Orientation}", settings.Orientation);
                _logger.LogDebug("Media Type {MediaType}", settings.MediaType);
                _logger.LogDebug("Margins Left: {Left}, Right: {Right}, Top: {Top}, Bottom: {Bottom}", settings.Margin.Left, settings.Margin.Right, settings.Margin.Top, settings.Margin.Bottom);

                settings.CommandLineArguments.Add("--no-sandbox");
                settings.CommandLineArguments.Add("--disable-setuid-sandbox");

                htmlConverter.ConverterSettings = settings;

                using var pdfDocumentStream = new MemoryStream();
                using var pdfDocument = htmlConverter.Convert(html, baseUrl);
                pdfDocument.Save(pdfDocumentStream);
                var pdfDocumentBytes = pdfDocumentStream.ToArray();
                pdfDocument.Close(true);

                return pdfDocumentBytes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<byte[]> RenderGotNotifyLetterPdf<T>(string templatePath, T model)
        {
            var html = await _razorRenderer.RenderView(templatePath, model);

            return await TransformToGotNotifyLetterPdf(html);
        }

        private async Task<byte[]> TransformToGotNotifyLetterPdf(string html)
        {
            var baseUrl = _webHostEnvironment.WebRootPath;

            var htmlConverter = new HtmlToPdfConverter();

            var settings = new BlinkConverterSettings
            {
                Orientation = PdfPageOrientation.Portrait,
                MediaType = MediaType.Print,

                Margin = new PdfMargins
                {
                    All = 0f
                },
                Scale = 1f
            };

            settings.PdfFooter = await CreateGovNotifyLetterHeaderFooter(Path.Combine("Views", "GovNotify", "Footer.cshtml"), baseUrl, settings);

            settings.CommandLineArguments.Add("--no-sandbox");
            settings.CommandLineArguments.Add("--disable-setuid-sandbox");

            htmlConverter.ConverterSettings = settings;

            using var pdfDocumentStream = new MemoryStream();
            var pdfDocument = htmlConverter.Convert(html, baseUrl);
            pdfDocument.Save(pdfDocumentStream);
            var pdfDocumentBytes = pdfDocumentStream.ToArray();
            pdfDocument.Close(true);

            return pdfDocumentBytes;
        }

        private async Task<PdfPageTemplateElement> CreateGovNotifyLetterHeaderFooter(string headerFooterTemplate, string baseUrl, BlinkConverterSettings converterSettings)
        {
            var htmlConverter = new HtmlToPdfConverter();

            const int footerContainerHeight = 148; // Pixels

            var settings = new BlinkConverterSettings
            {
                Orientation = PdfPageOrientation.Portrait,
                Margin = new PdfMargins { All = 0 },
                MediaType = MediaType.Print,
                Scale = 1f,
                PdfPageSize = new SizeF(converterSettings.PdfPageSize.Width, footerContainerHeight)
            };


            settings.CommandLineArguments.Add("--no-sandbox");
            settings.CommandLineArguments.Add("--disable-setuid-sandbox");

            htmlConverter.ConverterSettings = settings;

            var html = await _razorRenderer.RenderView(headerFooterTemplate, new object());

            var pdfDocument = htmlConverter.Convert(html, baseUrl);

            var bounds = new RectangleF(0, 0, converterSettings.PdfPageSize.Width, footerContainerHeight);
            var headerFooter = new PdfPageTemplateElement(bounds);
            headerFooter.Graphics.DrawPdfTemplate(pdfDocument.Pages[0].CreateTemplate(), bounds.Location, bounds.Size);

            return headerFooter;
        }
    }
}
