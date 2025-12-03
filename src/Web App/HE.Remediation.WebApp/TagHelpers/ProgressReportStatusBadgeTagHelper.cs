using Microsoft.AspNetCore.Razor.TagHelpers;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Extensions;

namespace HE.Remediation.WebApp.TagHelpers;

[HtmlTargetElement("progress-report-status-badge")]
public class ProgressReportStatusBadgeTagHelper : TagHelper
{
    [HtmlAttributeName("due")]
    public DateTime? DateDue { get; set; }

    [HtmlAttributeName("submitted")]
    public DateTime? DateSubmitted { get; set; }

    [HtmlAttributeName("show-date")]
    public bool? ShowDate { get; set; }

    [HtmlAttributeName("is-previous")]
    public bool? IsPrevious { get; set; }

    [HtmlAttributeName("suppress-due-status")]
    public bool SuppressDueStatus { get; set; } = false;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "strong";
        output.TagMode = TagMode.StartTagAndEndTag;
        var (status, date) = GetStatusAndDate();

        output.Attributes.SetAttribute("class", $"govuk-tag app-task-list__tag {GetCssClass(status)}");
        
        var statusText = Enum.GetName(status)?.SplitCamelCase();
        var dateText = ShowDate is true && date is not null ? " " + date.Value.ToString("dd MMM yyyy") : string.Empty;

        output.Content.SetHtmlContent($"{statusText}{dateText}");

        if(SuppressDueStatus && status == EProgressReportStatus.Due)
        {
            output.SuppressOutput();
        }
    }

    private (EProgressReportStatus, DateTime?) GetStatusAndDate()
    {
        if (DateSubmitted is not null)
        {
            return(EProgressReportStatus.Submitted, DateSubmitted.Value);
        }

        if (IsPrevious == true)
        {
            return (EProgressReportStatus.Expired, null);
        }

        if (DateDue is not null)
        {
            if (DateDue.Value.Date < DateTime.Today)
                return (EProgressReportStatus.Overdue, DateDue.Value);

            if (DateDue.Value.Date < DateTime.Today.AddDays(5))
                return (EProgressReportStatus.ToDo, DateDue.Value);

            return (EProgressReportStatus.Due, DateDue.Value);
        }

        return (0, null);
    }

    public string GetCssClass(EProgressReportStatus? status) =>
        status switch
        {
            EProgressReportStatus.Due => "govuk-tag--blue",
            EProgressReportStatus.Expired => "govuk-tag--red",
            EProgressReportStatus.Overdue => "govuk-tag--red",
            EProgressReportStatus.Submitted => "govuk-tag--grey",
            EProgressReportStatus.ToDo => "govuk-tag--blue",
            _ => ""
        };
}

