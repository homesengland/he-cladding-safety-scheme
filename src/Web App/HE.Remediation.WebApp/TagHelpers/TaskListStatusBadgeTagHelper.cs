using Microsoft.AspNetCore.Razor.TagHelpers;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Extensions;

namespace HE.Remediation.WebApp.TagHelpers
{
    [HtmlTargetElement("task-list-status-badge")]
    public class TaskListStatusBadgeTagHelper : TagHelper
    {
        [HtmlAttributeName("status")]
        public ETaskStatus TaskStatus { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "strong";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("class", $"govuk-tag app-task-list__tag {GetCssClass(TaskStatus)}");
            
            output.Content.SetHtmlContent(Enum.GetName(typeof(ETaskStatus), TaskStatus)!.SplitCamelCase());
        }

        public string GetCssClass(ETaskStatus status)
        {
            return status switch
            {
                ETaskStatus.NotStarted => "govuk-tag--grey",
                ETaskStatus.InProgress => "govuk-tag--blue",
                ETaskStatus.Completed => string.Empty, //Blank return the correct darker blue colour status per the GDS documentation
                _ => string.Empty
            };
        }
    }
}
