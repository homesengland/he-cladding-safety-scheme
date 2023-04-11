using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.TagHelpers
{
    [HtmlTargetElement("application-stages-list")]
    public class ApplicationStagesListTagHelper : TagHelper
    {
        [HtmlAttributeName("stage")]
        public EApplicationStage ApplicationStage { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ApplicationStagesListTagHelper";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            sb.AppendFormat($"<ul class=\"govuk-stage-list\">{RenderStagesList(ApplicationStage)}</ul>");

            output.PreContent.SetHtmlContent(sb.ToString());
        }

        public string RenderStagesList(EApplicationStage currentStage)
        {
            var sb = new StringBuilder();
            var stageMembersList = (EApplicationStage[])Enum.GetValues(typeof(EApplicationStage));
            foreach (var stage in stageMembersList)
            {
                var setStageSelected = (int)stage <= (int)currentStage ? "govuk-stage-list-item-selected" : "";
                sb.Append($"<li class=\"govuk-stage-list-item {setStageSelected} \">");
                    sb.Append("<div class=\"govuk-stage-list-item-bullet\"></div>");
                    sb.Append("<div class=\"govuk-stage-list-item-line\" ></div>");
                    sb.Append($"<span class=\"govuk-stage-list-item-text\">{GetStageName(stage)}</span>");
                sb.Append("</li>");
            }

            return sb.ToString();
        }

        public string GetStageName(EApplicationStage stage)
        {
            return stage switch
            {
                EApplicationStage.ApplyForGrant => "Apply for grant",
                EApplicationStage.SignGrantFundingAgreement => "Sign grant funding agreement",
                EApplicationStage.AddWorksPlan => "Add works plan",
                EApplicationStage.WorksStarted => "Works started",
                EApplicationStage.WorksCompleted => "Works completed",
                EApplicationStage.Variation => "Variation",
                _ => ""
            };
        }
    }
}
