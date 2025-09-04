using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class ManageProgrammeViewModel
    {
        public IReadOnlyCollection<ApplicationViewModel> ApplicationList { get; set; }

        public int PageCount { get; set; }

        public int CurrentPage { get; set; }

        public bool UseEllipses { get; set; }

        public bool ShowFilters { get; set; } = false;

        public bool FiltersSelected { get { 
                return SelectedInvestigationCompletionYearFilters.Any() || 
                    SelectedStartOnSiteYearFilters.Any() ||
                    SelectedPracticalCompletionYearFilters.Any() ||
                    SelectedSchemeTypeFilters.Any(); 
            } 
        }

        public List<EFinancialYearFilter> InvestigationCompletionYearFilters { get; set; } = [.. Enum.GetValues<EFinancialYearFilter>()];
        public IEnumerable<EFinancialYearFilter> SelectedInvestigationCompletionYearFilters = [];
        public IEnumerable<EFinancialYearFilter> UnselectedInvestigationCompletionYearFilters = [];

        public List<EFinancialYearFilter> StartOnSiteYearFilters { get; set; } = [.. Enum.GetValues<EFinancialYearFilter>()];
        public IEnumerable<EFinancialYearFilter> SelectedStartOnSiteYearFilters = [];
        public IEnumerable<EFinancialYearFilter> UnselectedStartOnSiteYearFilters = [];

        public List<EFinancialYearFilter> PracticalCompletionYearFilters { get; set; } = [.. Enum.GetValues<EFinancialYearFilter>()];
        public IEnumerable<EFinancialYearFilter> SelectedPracticalCompletionYearFilters = [];
        public IEnumerable<EFinancialYearFilter> UnselectedPracticalCompletionYearFilters = [];

        public List<EApplicationScheme> SchemeTypeFilters { get; set; } = [.. Enum.GetValues<EApplicationScheme>()];
        public IEnumerable<EApplicationScheme> SelectedSchemeTypeFilters = [];
        public IEnumerable<EApplicationScheme> UnselectedSchemeTypeFilters = [];


        public ManageProgrammeViewModel()
        {
            ApplicationList = new List<ApplicationViewModel>();
            PageCount = 1;
            CurrentPage = 1;
            UseEllipses = false;            
        }

        public class ApplicationViewModel
        {
            public Guid ApplicationId { get; set; }
            public string ApplicationNumber { get; set; }
            public string UniqueBuildingName { get; set; }
            public DateTime? InvestigationCompletionDate { get; set; }
            public DateTime? StartOnSiteDate { get; set; }
            public DateTime? PracticalCompletionDate { get; set; }
            public EApplicationScheme ApplicationScheme { get; set; }
        }
    }
}
