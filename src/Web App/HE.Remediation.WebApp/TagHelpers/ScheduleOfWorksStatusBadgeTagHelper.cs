using Microsoft.AspNetCore.Razor.TagHelpers;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.TagHelpers;

[HtmlTargetElement("schedule-of-works-status-badge")]
public class ScheduleOfWorksStatusBadgeTagHelper : TagHelper
{
    [HtmlAttributeName("application-status")]
    public EApplicationStatus? ApplicationStatus { get; set; }

    [HtmlAttributeName("is-approved")]
    public bool? IsApproved { get; set; }

    [HtmlAttributeName("is-submitted")]
    public bool? IsSubmitted { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var (statusText, cssClass) = GetStatusTextAndClass();
        if (statusText is null) return;

        output.TagName = "strong";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("class", $"govuk-tag app-task-list__tag {cssClass}");
        output.Content.SetHtmlContent($"{statusText}");
    }

    private (string, string) GetStatusTextAndClass()
    {
        if (ApplicationStatus == EApplicationStatus.WorksPackageApproved)
        {
            return ("To do", "govuk-tag--grey");
        }
        else if (ApplicationStatus == EApplicationStatus.ScheduleOfWorksInProgress)
        {
            return ("In progress", "govuk-tag--blue");
        }

        return default;
    }
}

