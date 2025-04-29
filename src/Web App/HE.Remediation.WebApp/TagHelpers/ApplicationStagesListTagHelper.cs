using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.TagHelpers
{
    [HtmlTargetElement("application-stages-list")]
    public class ApplicationStagesListTagHelper : TagHelper
    {
        private static Dictionary<EApplicationScheme, EApplicationStage[]> SchemeSpecificExcludeStates =
            new()
            {
                { 
                    EApplicationScheme.CladdingSafetyScheme, new EApplicationStage[] {
                                                                 EApplicationStage.BuildingComplete, 
                                                                 EApplicationStage.CounterSignInProgress
                    }
                },
                { 
                    EApplicationScheme.ResponsibleActorsScheme, new EApplicationStage[] {
                                                                 EApplicationStage.BuildingComplete, 
                                                                 EApplicationStage.CounterSignInProgress,
                                                                 EApplicationStage.GrantFundingAgreement
                    }
                },
                { 
                    EApplicationScheme.SocialSector, new EApplicationStage[] {
                                                                 EApplicationStage.BuildingComplete, 
                                                                 EApplicationStage.CounterSignInProgress,
                                                                 EApplicationStage.GrantFundingAgreement
                    }
                },
                { 
                    EApplicationScheme.SelfRemediating, new EApplicationStage[] {
                                                                 EApplicationStage.BuildingComplete, 
                                                                 EApplicationStage.CounterSignInProgress,
                                                                 EApplicationStage.GrantFundingAgreement
                    }
                },
            };

        [HtmlAttributeName("stage")]
        public EApplicationStage ApplicationStage { get; set; }

        [HtmlAttributeName("scheme")]
        public EApplicationScheme ApplicationScheme { get; set; } = EApplicationScheme.CladdingSafetyScheme;

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

                if (SchemeSpecificExcludeStates[ApplicationScheme].All(x => x != stage))
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
                EApplicationStage.ApplyForGrant => ApplicationScheme == EApplicationScheme.CladdingSafetyScheme ? "Apply for grant" : "Create building record",
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
