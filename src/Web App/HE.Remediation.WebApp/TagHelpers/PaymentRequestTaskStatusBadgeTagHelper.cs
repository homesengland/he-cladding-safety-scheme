using HE.Remediation.Core.Enums;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace HE.Remediation.WebApp.TagHelpers;

[HtmlTargetElement("payment-request-task-status-badge")]
public class PaymentRequestTaskStatusBadgeTagHelper : TagHelper
{
    [HtmlAttributeName("status")]
    public EPaymentRequestTaskStatus Status { get; set; }

    [HtmlAttributeName("expired")]
    public bool? IsExpired { get; set; }    
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {        
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;

        var sb = new StringBuilder();
        if (IsExpired == true)
        {
            sb.AppendFormat("<strong class=\"govuk-tag {1} govuk-!-margin-left-2\">{0}</strong>", 
                        GetStatusTextForExpired(), 
                        GetCssClassForExpired());
        }
        else
        {
            sb.AppendFormat("<strong class=\"govuk-tag {1} govuk-!-margin-left-2\">{0}</strong>", 
                        GetStatusText(Status), 
                        GetCssClassForStatus(Status));
        }        

        output.PreContent.SetHtmlContent(sb.ToString());
    }

    public string GetCssClassForExpired()
    {
        return "govuk-tag--orange";        
    }

    public string GetStatusTextForExpired()
    {
        return "EXPIRED";
    }

    public string GetCssClassForStatus(EPaymentRequestTaskStatus status)
    {
        switch (status)
        {
            case EPaymentRequestTaskStatus.NotStarted:
                return "govuk-tag--grey";
            case EPaymentRequestTaskStatus.InProgress:
                return "govuk-tag--orange";
            case EPaymentRequestTaskStatus.Submitted:
                return "govuk-tag--orange";
            case EPaymentRequestTaskStatus.Rejected:
                return "govuk-tag--orange";
            case EPaymentRequestTaskStatus.Completed:
                return "govuk-tag--green";            
            case EPaymentRequestTaskStatus.Paid:
                return "govuk-tag--green";            
        }

        return string.Empty;
    }

    public string GetStatusText(EPaymentRequestTaskStatus status)
    {
        switch (status)
        {
            case EPaymentRequestTaskStatus.NotStarted:
                return "TO DO";
            case EPaymentRequestTaskStatus.InProgress:
                return "In Progress";
            case EPaymentRequestTaskStatus.Submitted:
                return "Submitted";
            case EPaymentRequestTaskStatus.Completed:
                return "Approved"; //Blank return the correct darker blue colour status per the GDS documentation
            case EPaymentRequestTaskStatus.Rejected:
                return "Rejected";
            case EPaymentRequestTaskStatus.Paid:
                return "Paid";
        }
        return string.Empty;
    }
}
