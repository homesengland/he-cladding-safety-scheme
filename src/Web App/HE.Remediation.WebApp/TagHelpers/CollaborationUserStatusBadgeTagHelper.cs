using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Extensions;

namespace HE.Remediation.WebApp.TagHelpers
{
    [HtmlTargetElement("user-status-badge")]
    public class CollaborationUserStatusBadgeTagHelper : TagHelper
    {
        [HtmlAttributeName("status")]
        public ECollaborationUserStatus UserStatus { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            var description = UserStatus.GetEnumDisplayName() ?? Enum.GetName(typeof(ECollaborationUserStatus), UserStatus)!.SplitCamelCase();
            sb.AppendFormat("<strong class=\"govuk-tag {1}\">{0}</strong>", description, GetCssClass(UserStatus));

            output.PreContent.SetHtmlContent(sb.ToString());
        }

        public string GetCssClass(ECollaborationUserStatus status)
        {
            switch (status)
            {
                case ECollaborationUserStatus.AwaitingAdminConfirm:
                    return "govuk-tag--blue";
                case ECollaborationUserStatus.Invited: 
                case ECollaborationUserStatus.Accepted:
                    return "govuk-tag--green";
                case ECollaborationUserStatus.Rejected:
                case ECollaborationUserStatus.Removed:
                    return ""; //Blank return the correct darker blue colour status per the GDS documentation
            }
            return "";
        }
    }
}
