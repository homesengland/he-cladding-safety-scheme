using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.ManageProgramme
{
    public class GetManageProgrammeRequest : IRequest<IReadOnlyCollection<GetManageProgrammeResponse>>
    {
        public string Search { get; set; }
        public IEnumerable<EFinancialYearFilter> SelectedInvestigationCompletionYearFilters { get; set; } = [];
        public IEnumerable<EFinancialYearFilter> SelectedStartOnSiteYearFilters { get; set; } = [];
        public IEnumerable<EFinancialYearFilter> SelectedPracticalCompletionYearFilters { get; set; } = [];
        public IEnumerable<EApplicationScheme> SelectedSchemeTypeFilters { get; set; } = [];
    }
}
