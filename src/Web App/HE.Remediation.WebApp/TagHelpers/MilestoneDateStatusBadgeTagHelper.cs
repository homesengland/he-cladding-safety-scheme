using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HE.Remediation.WebApp.TagHelpers;

[HtmlTargetElement("milestone-date-status-badge")]
public class MilestoneDateStatusBadgeTagHelper : TagHelper
{
    [HtmlAttributeName("milestone-date")]
    public DateTime? MilestoneDate { get; set; }

    [HtmlAttributeName("is-submitted")]
    public bool IsSubmitted { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        var (@class, text) = GetClassAndText();

        var htmlOutput = $"<strong class=\"govuk-tag {@class} govuk-!-margin-left-2\">{text}</strong>";

        output.PreContent.SetHtmlContent(htmlOutput);
    }

    private (string Class, string Text) GetClassAndText()
    {
        if (!IsSubmitted)
        {
            return !MilestoneDate.HasValue 
                ? (Class: "govuk-tag--blue", Text: "To Do")
                : (Class: "govuk-tag--light-blue", Text: "In Progress");
        }

        return (Class: "govuk-tag--green", Text: "Complete");
    }
}