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

        [HtmlAttributeName("wrap")]
        public bool Wrap { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            var description = ApplicationStatus.GetEnumDisplayName() ?? Enum.GetName(typeof(EApplicationStatus), ApplicationStatus)!.SplitCamelCase();
            var nonWrappableSpace = Wrap ? " " : "&nbsp;";
            sb.AppendFormat("<strong class=\"govuk-tag app-task-list__tag {1}\">{0}</strong>", description.Replace(" ", nonWrappableSpace), GetCssClass(ApplicationStatus));

            output.PreContent.SetHtmlContent(sb.ToString());
        }

        public string GetCssClass(EApplicationStatus status)
        {
            switch (status)
            {
                case EApplicationStatus.ApplicationInProgress:
                    return "govuk-tag--blue";
                case EApplicationStatus.ApplicationInReview:
                    return "govuk-tag--blue";
                case EApplicationStatus.ApplicationApproved:
                    return "govuk-tag--green";
                case EApplicationStatus.Completed:
                    return ""; //Blank return the correct darker blue colour status per the GDS documentation
            }
            return "";
        }
    }
}
