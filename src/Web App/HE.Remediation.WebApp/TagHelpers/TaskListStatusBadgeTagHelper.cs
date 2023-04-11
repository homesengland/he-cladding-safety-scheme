using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
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
            output.TagName = "TaskListStatusBadgeTagHelper";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            sb.AppendFormat("<strong class=\"govuk-tag app-task-list__tag {1}\">{0}</strong>", Enum.GetName(typeof(ETaskStatus), TaskStatus)!.SplitCamelCase(), GetCssClass(TaskStatus));

            output.PreContent.SetHtmlContent(sb.ToString());
        }

        public string GetCssClass(ETaskStatus status)
        {
            switch (status)
            {
                case ETaskStatus.NotStarted:
                    return "govuk-tag--grey";
                case ETaskStatus.InProgress:
                    return "govuk-tag--blue";
                case ETaskStatus.Completed:
                    return ""; //Blank return the correct darker blue colour status per the GDS documentation
            }
            return "";
        }
    }
}
