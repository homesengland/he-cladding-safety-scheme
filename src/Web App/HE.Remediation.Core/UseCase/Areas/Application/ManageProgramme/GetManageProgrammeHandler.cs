using Dapper;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Application.ManageProgramme
{
    public class GetManageProgrammeHandler : IRequestHandler<GetManageProgrammeRequest, IReadOnlyCollection<GetManageProgrammeResponse>>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IManageProgrammeRepository _manageProgrammeRepository;

        public GetManageProgrammeHandler(IApplicationDataProvider applicationDataProvider, IManageProgrammeRepository manageProgrammeRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _manageProgrammeRepository = manageProgrammeRepository;
        }

        public async ValueTask<IReadOnlyCollection<GetManageProgrammeResponse>> Handle(GetManageProgrammeRequest request, CancellationToken cancellationToken)
        {
            var userId = _applicationDataProvider.GetUserId();
            var programmeApplications = await _manageProgrammeRepository.GetManageProgrammeDetails(request, userId);
            programmeApplications = ApplyDateRangeFilters(request, programmeApplications);
            return programmeApplications;
        }

        private IReadOnlyCollection<GetManageProgrammeResponse> ApplyDateRangeFilters(GetManageProgrammeRequest request, IReadOnlyCollection<GetManageProgrammeResponse> programmeApplications)
        {
            if (request.SelectedInvestigationCompletionYearFilters.Any())
            {
                var investigationCompletionDateRanges = GetDateRanges(request.SelectedInvestigationCompletionYearFilters);
                programmeApplications = programmeApplications.Where(a =>
                                                                        investigationCompletionDateRanges.Any(range =>
                                                                            (range.Item1 == null && a.InvestigationCompletionDate == null) ||
                                                                            (range.Item1 != null &&
                                                                                a.InvestigationCompletionDate >= range.Item1.Value.ToDateTime(TimeOnly.MinValue) &&
                                                                                a.InvestigationCompletionDate < range.Item2.Value.ToDateTime(TimeOnly.MinValue))
                                                                        )
                                                                    ).ToList();
            }


            if (request.SelectedStartOnSiteYearFilters.Any())
            {
                var startOnSiteDateRanges = GetDateRanges(request.SelectedStartOnSiteYearFilters);
                programmeApplications = programmeApplications.Where(a =>
                                                                        startOnSiteDateRanges.Any(range =>
                                                                            (range.Item1 == null && a.StartOnSiteDate == null) ||
                                                                            (range.Item1 != null &&
                                                                                a.StartOnSiteDate >= range.Item1.Value.ToDateTime(TimeOnly.MinValue) &&
                                                                                a.StartOnSiteDate < range.Item2.Value.ToDateTime(TimeOnly.MinValue))
                                                                        )
                                                                    ).ToList();
            }

            if (request.SelectedPracticalCompletionYearFilters.Any())
            {
                var practicalCompletionDateRanges = GetDateRanges(request.SelectedPracticalCompletionYearFilters);
                programmeApplications = programmeApplications.Where(a =>
                                                                        practicalCompletionDateRanges.Any(range =>
                                                                            (range.Item1 == null && a.PracticalCompletionDate == null) ||
                                                                            (range.Item1 != null &&
                                                                                a.PracticalCompletionDate >= range.Item1.Value.ToDateTime(TimeOnly.MinValue) &&
                                                                                a.PracticalCompletionDate < range.Item2.Value.ToDateTime(TimeOnly.MinValue))
                                                                        )
                                                                    ).ToList();
            }

            return programmeApplications;
        }

        private List<(DateOnly?, DateOnly?)> GetDateRanges(IEnumerable<EFinancialYearFilter> financialYears)
        {
            var dateRanges = new List<(DateOnly?, DateOnly?)>();

            foreach (var fy in financialYears)
            {
                DateOnly? start = null, end = null;

                if(fy == EFinancialYearFilter.FY24_25)
                {
                    start = new DateOnly(2024, 4, 1);
                }

                if (fy == EFinancialYearFilter.FY25_26)
                {
                    start = new DateOnly(2025, 4, 1);
                }

                if (fy == EFinancialYearFilter.FY26_27)
                {
                    start = new DateOnly(2026, 4, 1);
                }

                if (fy == EFinancialYearFilter.FY27_28)
                {
                    start = new DateOnly(2027, 4, 1);
                }

                if(start != null)
                {
                    end = start.Value.AddYears(1);
                }
                
                if (fy == EFinancialYearFilter.FY28_29)
                {
                    start = new DateOnly(2028, 4, 1);
                    end = DateOnly.MaxValue; // open ended
                }

                if (fy == EFinancialYearFilter.None)
                {
                    start = null;
                    end = null; 
                }

                dateRanges.Add((start, end));
            }

            return dateRanges;
        }
    }
}
