using HE.Remediation.Core.Enums;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace HE.Remediation.WebApp.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("results-small-pager")]
    public class SmallPagerTagHelper : TagHelper
    {
        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }

        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }

        [HtmlAttributeName("uri-prefix")]
        public string URIPrefix { get; set; }

        [HtmlAttributeName("search-parameter")]
        public string SearchParameter { get; set; }

        [HtmlAttributeName("show-filters")]
        public bool ShowFilters { get; set; }

        [HtmlAttributeName("selected-filter-stages")]
        public IEnumerable<EApplicationStage> SelectedFilterStageOptions { get; set; } = [];

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
             
            if ((CurrentPage == 1) && (PageCount == 1))
            {
                output.PreContent.SetHtmlContent(string.Empty);
                return;
            }

            var sb = new StringBuilder(Environment.NewLine);
            sb.Append(OutputPagerTitle());
            if (CurrentPage > 1)
            {
                sb.Append(ObtainPreviousLinkMarkup());
            }

            sb.Append(OutputPageEntriesStart());            

            if (PageCount < 10)
            {
                for (int PageCounter = 1; PageCounter < PageCount + 1; PageCounter++)
                {
                    sb.Append(ObtainPageMarkup((PageCounter == CurrentPage), PageCounter));                    
                }                
            }
            else
            {                
                //sb.Append(ObtainPageMarkup(1));

                //if (CurrentPage >= 5)
                //{
                //    sb.Append(ObtainEllipsesMarkup());
                //    //sb.Append(ObtainPageMarkup(2));
                //}
                //else
                //{
                //}

                //if (CurrentPage > 2)
                //{
                //    sb.Append(ObtainPageMarkup(CurrentPage-1));        
                //}
                //if (CurrentPage > 1)
                //{
                //    sb.Append(ObtainPageMarkup(CurrentPage));
                //}
                //if (CurrentPage < PageCount)
                //{
                //    sb.Append(ObtainPageMarkup(CurrentPage+1));        
                //}

                //if (PageCount >= 4)
                //{
                //    sb.Append(ObtainEllipsesMarkup());
                //}
                //sb.Append(ObtainPageMarkup(PageCount));
            }
            
            sb.Append(OutputPageEntriesEnd());

            if (CurrentPage < PageCount)
            {
                sb.Append(ObtainNextLinkMarkup());
            }
            sb.Append(OutputPagerFooter());
            output.PreContent.SetHtmlContent(sb.ToString());
        }

        private string ObtainEllipsesMarkup()
        {
            return "<li class=\"govuk-pagination__item govuk-pagination__item--ellipses\">&ctdot;</li>";
        }

        private string OutputPageEntriesEnd()
        {
            return "  </ul>" + Environment.NewLine;
        }

        private string OutputPageEntriesStart()
        {
            return "  <ul class=\"govuk-pagination__list\">" + Environment.NewLine;
        }
        
        private string OutputPagerFooter()
        {
            return "</nav>" + Environment.NewLine;
        }

        private string OutputPagerTitle()
        {
            return "<nav class=\"govuk-pagination\" role=\"navigation\" aria-label=\"results\">" + Environment.NewLine;
        }

        private string ObtainPageMarkup(int PageNo)
        {
            return ObtainPageMarkup((PageNo == CurrentPage), PageNo);
        }

        private string ObtainPageMarkup(bool ItemSelected, int PageNo)
        {
            var sb = new StringBuilder();            
            if (ItemSelected)
            {
                sb.Append("    <li class=\"govuk-pagination__item govuk-pagination__item--current\">" + Environment.NewLine);
            }
            else
            {
                sb.Append("    <li class=\"govuk-pagination__item\">" + Environment.NewLine);
            }
            sb.Append("      <a class=\"govuk-link govuk-pagination__link\" href=\"");
            var url = new UriBuilder(URIPrefix).AddParameter("pageNo", PageNo).AddParameter("search", SearchParameter);
            sb.Append(url.Build());
            sb.Append($"&=ShowFilters={ShowFilters}");
            foreach (var stageOption in SelectedFilterStageOptions)
            {
                sb.Append($"&selectedFilterStages={stageOption}");
            }
            sb.Append("\" aria-label=\"Page ");
            sb.Append(PageNo.ToString());
            sb.Append("\"");
            if (ItemSelected)
            {
                sb.Append(" aria-current=\"page\">");                
            }
            else
            {
                sb.Append(">"); 
            }
            sb.Append(PageNo.ToString());
            sb.Append("</a>" + Environment.NewLine);
            sb.Append("    </li>" + Environment.NewLine);                                        
            return sb.ToString();
        }

        private string ObtainPreviousLinkMarkup()
        {
            var sb = new StringBuilder();

            sb.Append("  <div class=\"govuk-pagination__prev\">" + Environment.NewLine);
            sb.Append("    <a class=\"govuk-link govuk-pagination__link\" href=\"");
            var url = new UriBuilder(URIPrefix).AddParameter("pageNo", CurrentPage - 1).AddParameter("search", SearchParameter);
            sb.Append(url.Build());
            sb.Append($"&=ShowFilters={ShowFilters}");
            foreach (var stageOption in SelectedFilterStageOptions)
            {
                sb.Append($"&selectedFilterStages={stageOption}");
            }
            sb.Append("\" rel=\"prev\">" + Environment.NewLine);            
            sb.Append("      <svg class=\"govuk-pagination__icon govuk-pagination__icon--prev\" ");
            sb.Append("xmlns=\"http://www.w3.org/2000/svg\" height=\"13\" width=\"15\" aria-hidden=\"true\" focusable=\"false\" viewBox=\"0 0 15 13\">" + Environment.NewLine);
            sb.Append("        <path d=\"m6.5938-0.0078125-6.7266 6.7266 6.7441 6.4062 1.377-1.449-4.1856-3.9768h12.896v-2h-12.984l4.2931-4.293-1.414-1.414z\">");
            sb.Append("</path>" + Environment.NewLine);
            sb.Append("      </svg>" + Environment.NewLine);
            sb.Append("      <span class=\"govuk-pagination__link-title\">Previous</span>" + Environment.NewLine);
            sb.Append("    </a>" + Environment.NewLine);
            sb.Append("  </div>" + Environment.NewLine);
            return sb.ToString();
        }

        private string ObtainNextLinkMarkup()
        {
            var sb = new StringBuilder();
            sb.Append("  <div class=\"govuk-pagination__next\">" + Environment.NewLine);
            sb.Append("    <a class=\"govuk-link govuk-pagination__link\" href=\"");
            var url = new UriBuilder(URIPrefix).AddParameter("pageNo", CurrentPage + 1).AddParameter("search", SearchParameter);
            sb.Append(url.Build());
            sb.Append($"&=ShowFilters={ShowFilters}");
            foreach (var stageOption in SelectedFilterStageOptions)
            {
                sb.Append($"&selectedFilterStages={stageOption}");
            }
            sb.Append("\" rel=\"next\">"  + Environment.NewLine);
            sb.Append("      <span class=\"govuk-pagination__link-title\">Next</span>"  + Environment.NewLine);
            sb.Append("      <svg class=\"govuk-pagination__icon govuk-pagination__icon--next\" ");
            sb.Append("xmlns=\"http://www.w3.org/2000/svg\" height=\"13\" width=\"15\" ");
            sb.Append("aria-hidden=\"true\" focusable=\"false\" viewBox=\"0 0 15 13\">"  + Environment.NewLine);
            sb.Append("        <path d=\"m8.107-0.0078125-1.4136 1.414 4.2926 4.293h-12.986v2h12.896l-4.1855 ");
            sb.Append("3.9766 1.377 1.4492 6.7441-6.4062-6.7246-6.7266z\"></path>"  + Environment.NewLine);
            sb.Append("      </svg>" + Environment.NewLine) ;
            sb.Append("    </a>" + Environment.NewLine);
            sb.Append("  </div>" + Environment.NewLine);
            return sb.ToString();
        }        

        private class UriBuilder
        {
            private string baseUri;
            private readonly IDictionary<string, string> parameters;

            public UriBuilder()
            {
                parameters = new Dictionary<string, string>();
            }

            public UriBuilder(string baseUri) : this()
            {
                SetBase(baseUri);
            }

            public UriBuilder SetBase(string baseUri)
            {
                this.baseUri = baseUri;
                return this;
            }

            public UriBuilder AddParameter(string name, object value)
            {
                parameters.Add(name, value is not null ? value.ToString() : string.Empty);
                return this;
            }

            public string Build()
            {
                return QueryHelpers.AddQueryString(baseUri, parameters);
            }
        }
    }
}
