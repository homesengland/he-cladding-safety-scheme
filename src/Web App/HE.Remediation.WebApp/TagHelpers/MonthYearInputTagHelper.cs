using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

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
        var month = value?.Month?.ToString() ?? "";
        var year = value?.Year?.ToString() ?? "";

        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        var fieldsToCheck = new[] { baseName, $"{baseName}.Month", $"{baseName}.Year" };
        var erroredFields = ModelState.Keys.Where(k => fieldsToCheck.Contains(k) && ModelState[k].Errors.Any());
        var errors = erroredFields.SelectMany(ef => ModelState[ef].Errors).OrderBy(e => e.ErrorMessage);

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
        sb.AppendLine($"<input class=\"govuk-input govuk-date-input__input govuk-input--width-2\" id=\"{baseName}_Month\" name=\"{baseName}.Month\" type=\"text\" inputmode=\"numeric\" value=\"{month}\" />");
        sb.AppendLine("</div></div>");

        // Year
        sb.AppendLine("<div class=\"govuk-date-input__item\">");
        sb.AppendLine("<div class=\"govuk-form-group\">");
        sb.AppendLine($"<label class=\"govuk-label govuk-date-input__label\" for=\"{baseName}_Year\">Year</label>");
        sb.AppendLine($"<input class=\"govuk-input govuk-date-input__input govuk-input--width-4\" id=\"{baseName}_Year\" name=\"{baseName}.Year\" type=\"text\" inputmode=\"numeric\" value=\"{year}\" />");
        sb.AppendLine("</div></div>");

        // Day (default to 1)
        sb.AppendLine($"<input type=\"hidden\" id=\"{baseName}_Day\" name=\"{baseName}.Day\" value=\"1\" />");

        sb.AppendLine("</div>");

        sb.AppendLine("</fieldset>");

        foreach (var error in errors)
        {
            sb.AppendLine($"<span class=\"govuk-error-message\">{error.ErrorMessage}</span>");
        }

        output.Content.SetHtmlContent(sb.ToString());
    }

    public class MonthYearInput
    {
        public string Month { get; set; }
        public string Year { get; set; }

        public DateTime? ToDateTime()
        {
            _ = int.TryParse(Month, out int month);
            _ = int.TryParse(Year, out int year);
            if ((month >= 1 && month <= 12) && (year >= 1))
            {
                return new DateTime(year, month, 1);
            }
            return null;
        }
    }
}
