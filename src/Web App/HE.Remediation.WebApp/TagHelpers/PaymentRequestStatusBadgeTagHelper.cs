using HE.Remediation.Core.Enums;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace HE.Remediation.WebApp.TagHelpers;

[HtmlTargetElement("payment-request-status-badge")]
public class PaymentRequestStatusBadgeTagHelper : TagHelper
{
    [HtmlAttributeName("status")]
    public EPaymentRequestStatus Status { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        var sb = new StringBuilder();
        sb.AppendFormat("<strong class=\"govuk-tag {1} govuk-!-margin-left-2\">{0}</strong>",
                        GetStatusText(Status),
                        GetCssClassForStatus(Status));

        output.PreContent.SetHtmlContent(sb.ToString());
    }

    public string GetCssClassForStatus(EPaymentRequestStatus status)
    {
        switch (status)
        {
            case EPaymentRequestStatus.Paid:
            case EPaymentRequestStatus.Approved:
                return "govuk-tag--green";
            case EPaymentRequestStatus.Missed:
                return "govuk-tag--orange";
            case EPaymentRequestStatus.Rejected:
                return "govuk-tag--orange";
            case EPaymentRequestStatus.Todo:
                return "govuk-tag--grey";
            case EPaymentRequestStatus.Reclaimed:
                return "govuk-tag--blue";
        }

        return string.Empty;
    }

    public string GetStatusText(EPaymentRequestStatus status)
    {
        switch (status)
        {
            case EPaymentRequestStatus.Paid:
                return "Paid";
            case EPaymentRequestStatus.Missed:
                return "Missed";
            case EPaymentRequestStatus.Rejected:
                return "Rejected";
            case EPaymentRequestStatus.Todo:
                return "TO DO";
            case EPaymentRequestStatus.Approved:
                return "Approved";
            case EPaymentRequestStatus.Reclaimed:
                return "Reclaimed";
        }
        return string.Empty;
    }
}
