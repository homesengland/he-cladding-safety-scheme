using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HE.Remediation.WebApp.TagHelpers;

[HtmlTargetElement("input-text-box")]
public class InputTextBoxTagHelper : TagHelper
{
    [HtmlAttributeName("disabled")]
    public bool Disabled { get; set; }

    [HtmlAttributeName("asp-for")]
    public ModelExpression AspFor { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "input";
        output.TagMode = TagMode.StartTagAndEndTag;
        
        if (AspFor?.Name != null)
        {
            output.Attributes.SetAttribute("id", AspFor?.Name);
            output.Attributes.SetAttribute("name", AspFor?.Name);
        }
                
        if (Disabled)
        { 
            output.Attributes.SetAttribute("disabled", "disabled");        
        }           
    }
}
