using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace HE.Remediation.WebApp.TagHelpers
{
    [HtmlTargetElement("task-status-badge")]
    public class TaskStatusBadgeTagHelper : TagHelper
    {
        [HtmlAttributeName("task-status")]
        public string Status { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "strong";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("class", $"govuk-tag app-task-list__tag {GetCssClass(Status)}");
            
            output.Content.SetHtmlContent(Status);
        }

        public string GetCssClass(string status)
        {
            return status switch
            {
                "New" => "govuk-tag--blue",
                _ => string.Empty
            };
        }
    }
}
