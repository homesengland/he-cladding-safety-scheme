using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using static GovUk.Frontend.AspNetCore.ComponentDefaults;

namespace HE.Remediation.WebApp.TagHelpers;

[HtmlTargetElement("month-year-input")]
public class MonthYearInputTagHelper : TagHelper
{
    [HtmlAttributeName("asp-for")]
    public ModelExpression AspFor { get; set; }

    [HtmlAttributeName("label")]
    public string Label { get; set; }

    [HtmlAttributeName("model-state")]
    public ModelStateDictionary ModelState { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var baseName = AspFor?.Name ?? "MonthYear";
        var value = AspFor?.Model as MonthYearInput;
        var month = value?.Month.ToString() ?? "";
        var year = value?.Year.ToString() ?? "";

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        var errors = ModelState != null && ModelState.ContainsKey(AspFor.Name) ? ModelState[AspFor.Name].Errors : [];
        var formGroupClass = "govuk-form-group" + (errors.Any() ? " govuk-form-group--error" : "");
        output.Attributes.SetAttribute("class", formGroupClass);
        var sb = new StringBuilder();

        sb.AppendLine("<fieldset class=\"govuk-fieldset\">");
        sb.AppendLine($"<legend class=\"govuk-fieldset__legend govuk-fieldset__legend--s\">{Label}</legend>");
        sb.AppendLine($"<span class=\"govuk-hint\">For example, 8 2022</span>");
        sb.AppendLine($"<div class=\"govuk-date-input\" id=\"{baseName}\">");

        // Month
        sb.AppendLine("<div class=\"govuk-date-input__item\">");
        sb.AppendLine("<div class=\"govuk-form-group\">");
        sb.AppendLine($"<label class=\"govuk-label govuk-date-input__label\" for=\"{baseName}_Month\">Month</label>");
        sb.AppendLine($"<input class=\"govuk-input govuk-date-input__input govuk-input--width-2\" id=\"{baseName}_Month\" name=\"{baseName}.Month\" type=\"text\" pattern=\"[0-9]*\" inputmode=\"numeric\" value=\"{month}\" />");
        sb.AppendLine("</div></div>");

        // Year
        sb.AppendLine("<div class=\"govuk-date-input__item\">");
        sb.AppendLine("<div class=\"govuk-form-group\">");
        sb.AppendLine($"<label class=\"govuk-label govuk-date-input__label\" for=\"{baseName}_Year\">Year</label>");
        sb.AppendLine($"<input class=\"govuk-input govuk-date-input__input govuk-input--width-4\" id=\"{baseName}_Year\" name=\"{baseName}.Year\" type=\"text\" pattern=\"[0-9]*\" inputmode=\"numeric\" value=\"{year}\" />");
        sb.AppendLine("</div></div>");

        // Day (default to 1)
        sb.AppendLine($"<input type=\"hidden\" id=\"{baseName}_Day\" name=\"{baseName}.Day\" value=\"1\" />");

        sb.AppendLine("</div>");
        
        sb.AppendLine("</fieldset>");

        if (errors.Any())
        {
            sb.AppendLine($"<span class=\"govuk-error-message\">{errors.FirstOrDefault()?.ErrorMessage}</span>");
        }

        output.Content.SetHtmlContent(sb.ToString());
    }

    public class MonthYearInput
    {
        public int? Month { get; set; }
        public int? Year { get; set; }

        public DateTime? ToDateTime()
        {
            if (Month.HasValue && (Month >= 1 && Month <= 12) && Year.HasValue && (Year >= 1))
            {
                return new DateTime(Year.Value, Month.Value, 1);
            }
            return null;
        }
    }
}
