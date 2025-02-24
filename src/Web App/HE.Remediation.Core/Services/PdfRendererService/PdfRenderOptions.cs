using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

namespace HE.Remediation.Core.Services.PdfRendererService;

public class PdfRenderOptions
{
    public PdfPageOrientation? Orientation { get; set; } = null;
    public MediaType? MediaType { get; set; } = null;
    public PdfMargins Margins { get; set; } = null;
}