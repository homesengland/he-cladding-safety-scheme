using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Extensions;

namespace HE.Remediation.WebApp.TagHelpers
{
    [HtmlTargetElement("application-stage-text")]
    public class ApplicationStageTagHelper : TagHelper
    {
        [HtmlAttributeName("stage")]
        public EApplicationStage ApplicationStage { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ApplicationStageTagHelper";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            sb.AppendFormat($"<span class=\"govuk-body\">{GetStageName(ApplicationStage)}</span>");

            output.PreContent.SetHtmlContent(sb.ToString());
        }

        public string GetStageName(EApplicationStage stage)
        {
            switch (stage)
            {
                case EApplicationStage.ApplyForGrant:
                    return "Apply for grant";
                case EApplicationStage.SignGrantFundingAgreement:
                    return "Sign grant funding agreement";
                case EApplicationStage.AddWorksPlan:
                    return "Add works plan";
                case EApplicationStage.WorksStarted:
                    return "Works started";
                case EApplicationStage.WorksCompleted:
                    return "Works completed";
                case EApplicationStage.Variation:
                    return "Variation";
            }
            return "";
        }
    }
}
