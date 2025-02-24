using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.TagHelpers
{
    [HtmlTargetElement("application-stage-text")]
    public class ApplicationStageTagHelper : TagHelper
    {
        [HtmlAttributeName("stage")]
        public EApplicationStage ApplicationStage { get; set; }

        [HtmlAttributeName("text-only")]
        public bool? TextOnly { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            if (TextOnly == true)
            {
                sb.AppendFormat(GetStageName(ApplicationStage));
            }
            else
            {
                sb.AppendFormat($"<span class=\"govuk-body\">{GetStageName(ApplicationStage)}</span>");
            }

            output.PreContent.SetHtmlContent(sb.ToString());
        }

        public string GetStageName(EApplicationStage stage)
        {
            switch (stage)
            {
                case EApplicationStage.ApplyForGrant:
                    return "Apply for grant";
                case EApplicationStage.GrantFundingAgreement:
                    return "Grant funding agreement";
                case EApplicationStage.WorksPackage:
                    return "Works package";
                case EApplicationStage.WorksDelivery:
                    return "Works delivery";
                case EApplicationStage.WorksCompleted:
                    return "Works completion";
                case EApplicationStage.BuildingComplete:
                    return "Building complete";
                case EApplicationStage.Closed:
                    return "Closed";
            }
            return "";
        }
    }
}
