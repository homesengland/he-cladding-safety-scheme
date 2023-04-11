using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Extensions;

namespace HE.Remediation.WebApp.TagHelpers
{
    [HtmlTargetElement("application-status-badge")]
    public class ApplicationStatusBadgeTagHelper : TagHelper
    {
        [HtmlAttributeName("status")]
        public EApplicationStatus ApplicationStatus { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ApplicationStatusBadgeTagHelper";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            sb.AppendFormat("<strong class=\"govuk-tag app-task-list__tag {1}\">{0}</strong>", Enum.GetName(typeof(EApplicationStatus), ApplicationStatus)!.SplitCamelCase(), GetCssClass(ApplicationStatus));

            output.PreContent.SetHtmlContent(sb.ToString());
        }

        public string GetCssClass(EApplicationStatus status)
        {
            switch (status)
            {
                case EApplicationStatus.InProgress:
                    return "govuk-tag--blue";
                case EApplicationStatus.InReview:
                    return "govuk-tag--blue";
                case EApplicationStatus.Approved:
                    return "govuk-tag--green";
                case EApplicationStatus.Completed:
                    return ""; //Blank return the correct darker blue colour status per the GDS documentation
            }
            return "";
        }
    }
}
