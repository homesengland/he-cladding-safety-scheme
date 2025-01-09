using HE.Remediation.Core.Enums;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace HE.Remediation.WebApp.TagHelpers
{
    [HtmlTargetElement("payment-status-badge")]
    public class PaymentStatusBadgeTagHelper : TagHelper
    {
        [HtmlAttributeName("status")]
        public EPaymentStatus? Status { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Status is null or EPaymentStatus.Undefined) return;

            output.TagName = "strong";
            output.TagMode = TagMode.StartTagAndEndTag;
            
            output.Attributes.SetAttribute("class", $"govuk-tag {GetCssClassForStatus(Status)} govuk-!-margin-left-2");
            output.Content.SetHtmlContent(GetStatusText(Status));
        }

        public string GetCssClassForStatus(EPaymentStatus? status)
        {
            return status switch
            {
                EPaymentStatus.Paid => "govuk-tag--green",
                EPaymentStatus.Missed => "govuk-tag--orange",
                EPaymentStatus.Rejected => "govuk-tag--orange",
                EPaymentStatus.Approved => "govuk-tag--green",
                EPaymentStatus.Recommended => "govuk-tag--green",
                _ => string.Empty,
            };
        }

        public string GetStatusText(EPaymentStatus? status)
        {
            return status switch
            {
                EPaymentStatus.Paid => "Paid",
                EPaymentStatus.Missed => "Missed",
                EPaymentStatus.Rejected => "Rejected",
                EPaymentStatus.Approved => "Approved",
                EPaymentStatus.Recommended => "Recommended",
                _ => string.Empty,
            };
        }
    }
}
