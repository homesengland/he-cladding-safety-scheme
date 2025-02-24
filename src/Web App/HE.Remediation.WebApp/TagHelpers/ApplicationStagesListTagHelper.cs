using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.TagHelpers
{
    [HtmlTargetElement("application-stages-list")]
    public class ApplicationStagesListTagHelper : TagHelper
    {
        private static EApplicationStage[] ExcludedStates =
        {
            EApplicationStage.BuildingComplete, 
            EApplicationStage.CounterSignInProgress
        };

        [HtmlAttributeName("stage")]
        public EApplicationStage ApplicationStage { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("class", "govuk-stage-list");
            
            output.Content.SetHtmlContent(RenderStagesList(ApplicationStage));
        }

        public string RenderStagesList(EApplicationStage currentStage)
        {
            var sb = new StringBuilder();            
            var stageMembersList = (EApplicationStage[])Enum.GetValues(typeof(EApplicationStage));
            foreach (var stage in stageMembersList)
            {
                var setStageSelected = string.Empty;
                if ((int)stage < (int)currentStage)
                {
                    setStageSelected = "govuk-stage-list-item-selected";
                }
                else if (((int)stage == (int)currentStage))
                {
                    setStageSelected = "govuk-stage-list-item-current";
                }

                if (ExcludedStates.All(x => x != stage))
                {
                    if (currentStage == EApplicationStage.BuildingComplete)
                    {
                        setStageSelected = "govuk-stage-list-item-selected";
                    }
                    
                    sb.Append($"<li class=\"govuk-stage-list-item {setStageSelected} \">");
                    sb.Append("<div class=\"govuk-stage-list-item-bullet\"></div>");
                    sb.Append("<div class=\"govuk-stage-list-item-line\" ></div>");
                    sb.Append($"<span class=\"govuk-stage-list-item-text\">{GetStageName(stage)}</span>");
                    sb.Append("</li>");
                }                        
            }

            return sb.ToString();
        }
        
        public string GetStageName(EApplicationStage stage)
        {
            return stage switch
            {
                EApplicationStage.ApplyForGrant => "Apply for grant",
                EApplicationStage.GrantFundingAgreement => "Grant funding agreement",
                EApplicationStage.WorksPackage => "Works package",
                EApplicationStage.WorksDelivery => "Works delivery",
                EApplicationStage.WorksCompleted => "Works completion",
                EApplicationStage.BuildingComplete => "Building complete",
                EApplicationStage.Closed => "Closed",
                _ => ""
            };
        }
    }
}
