using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.Application.ManageProgramme;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.Helpers;
using HE.Remediation.WebApp.ViewModels.Application;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace HE.Remediation.WebApp.Areas.Application.Controllers
{
    [Area("Application")]
    [CookieAuthorise]
    public class ManageProgrammeController : Controller
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public ManageProgrammeController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        #region "Grid"

        [HttpGet]
        public async Task<IActionResult> Index(ManageProgrammeDto request)
        {
            const int pageSize = 10;
            request.PageNo ??= 1;

            var viewModel = new ManageProgrammeViewModel();

            var existingApplicationsResponse = await FilterStages(request, viewModel);

            var applications = _mapper.Map<IReadOnlyCollection<ManageProgrammeViewModel.ApplicationViewModel>>(existingApplicationsResponse);

            var page = PaginationHelper.ObtainPageHandlingDetails(request.PageNo.Value, applications.Count, pageSize, false);

            viewModel.CurrentPage = page.CurrentPage;
            viewModel.ApplicationList = applications.Skip(page.StartRecordValue).Take(pageSize).ToArray();
            viewModel.PageCount = page.NoOfPages;
            viewModel.UseEllipses = page.UseEllipses;

            return View(viewModel);
        }

        private async Task<IReadOnlyCollection<GetManageProgrammeResponse>> FilterStages(ManageProgrammeDto dto, ManageProgrammeViewModel viewModel)
        {
            viewModel.SelectedInvestigationCompletionYearFilters = dto.SelectedWic;
            viewModel.SelectedStartOnSiteYearFilters = dto.SelectedSos;
            viewModel.SelectedPracticalCompletionYearFilters = dto.SelectedPc;
            viewModel.SelectedSchemeTypeFilters = dto.SelectedScheme;

            viewModel.ShowFilters = dto.ShowFilters;

            if (dto.Source.Contains("showFilters"))
            {
                viewModel.ShowFilters = !dto.ShowFilters;
            }

            if (dto.Source.Contains("clear"))
            {
                viewModel.SelectedInvestigationCompletionYearFilters = [];
                viewModel.SelectedStartOnSiteYearFilters = [];
                viewModel.SelectedPracticalCompletionYearFilters = [];
                viewModel.SelectedSchemeTypeFilters = [];
            }
            else if (dto.Source.Contains("unselect"))
            {
                viewModel.SelectedInvestigationCompletionYearFilters = dto.SelectedWic.Except(dto.UnselectedWic);
                viewModel.SelectedStartOnSiteYearFilters = dto.SelectedSos.Except(dto.UnselectedSos);
                viewModel.SelectedPracticalCompletionYearFilters = dto.SelectedPc.Except(dto.UnselectedPc);
                viewModel.SelectedSchemeTypeFilters = dto.SelectedScheme.Except(dto.UnselectedScheme);
            }

            if (dto.Source.Contains("applyFilters"))
            {
                viewModel.ShowFilters = false;
            }

            var existingApplicationsResponse = await _sender.Send(new GetManageProgrammeRequest
            {
                Search = dto.Search,
                SelectedInvestigationCompletionYearFilters = viewModel.SelectedInvestigationCompletionYearFilters,
                SelectedStartOnSiteYearFilters = viewModel.SelectedStartOnSiteYearFilters,
                SelectedPracticalCompletionYearFilters = viewModel.SelectedPracticalCompletionYearFilters,
                SelectedSchemeTypeFilters = viewModel.SelectedSchemeTypeFilters,
            });

            return existingApplicationsResponse;
        }

        [HttpPost]
        public IActionResult Index(UpdateDto dto)
        {
            if (dto.AppId == null || dto.AppId.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Please select at least one application.");
                return View(new ManageProgrammeViewModel());
            }
            return RedirectToAction("About", dto);
        }

        #endregion

        #region "Info/About"

        [HttpGet]
        public IActionResult About(UpdateDto dto)
        {
            var model = new ManageProgrammeAboutViewModel()
            {
                ApplicationIds = dto.AppId
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AboutContinue(UpdateDto dto)
        {
            return RedirectToAction("Update", dto);
        }

        #endregion

        #region "Update"

        [HttpGet]
        public IActionResult Update(UpdateDto dto)
        {
            var model = new ManageProgrammeUpdateViewModel
            {
                EstimatedInvestigationCompletionDate = ParseExactDateTime(dto.EstimatedInvestigationCompletionDate),
                EstimatedStartOnSiteDate = ParseExactDateTime(dto.EstimatedStartOnSiteDate),
                EstimatedPracticalCompletionDate = ParseExactDateTime(dto.EstimatedPracticalCompletionDate),
                AppId = dto.AppId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ManageProgrammeUpdateViewModel model, ESubmitAction? submitAction)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var queryStringFriendlyDto = new UpdateDto()
            {
                EstimatedInvestigationCompletionDate = model.EstimatedInvestigationCompletionDate?.ToString(DATE_FORMAT),
                EstimatedStartOnSiteDate = model.EstimatedStartOnSiteDate?.ToString(DATE_FORMAT),
                EstimatedPracticalCompletionDate = model.EstimatedPracticalCompletionDate?.ToString(DATE_FORMAT),
                AppId = [.. model.AppId]
            };

            if (submitAction == ESubmitAction.Continue)
            {
                var validator = new ManageProgrammeUpdateViewModelValidator();

                var validationResult = await validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    validationResult.AddToModelState(ModelState, string.Empty);
                    return View(model);
                }

                return RedirectToAction("CheckYourAnswers", queryStringFriendlyDto);
            }

            // go back
            return RedirectToAction("About", queryStringFriendlyDto);
        }

        #endregion

        #region "CheckYourAnswers"

        [HttpGet]
        public async Task<IActionResult> CheckYourAnswers(UpdateDto dto)
        {
            var headlines = await _sender.Send(new GetApplicationHeadlinesRequest() { ApplicationIds = dto.AppId });

            var model = new ManageProgrammeUpdateViewModel
            {
                ApplicationHeadlines = headlines.Items,
                EstimatedInvestigationCompletionDate = ParseExactDateTime(dto.EstimatedInvestigationCompletionDate),
                EstimatedStartOnSiteDate = ParseExactDateTime(dto.EstimatedStartOnSiteDate),
                EstimatedPracticalCompletionDate = ParseExactDateTime(dto.EstimatedPracticalCompletionDate),
                AppId = dto.AppId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitCheckYourAnswers(UpdateDto dto, ESubmitAction? submitAction)
        {
            if(submitAction == ESubmitAction.Continue)
            {
                var request = new SetApplicationUpdatesRequest()
                {
                    ApplicationIds = dto.AppId,
                    EstimatedInvestigationCompletionDate = ParseExactDateTime(dto.EstimatedInvestigationCompletionDate),
                    EstimatedStartOnSiteDate = ParseExactDateTime(dto.EstimatedStartOnSiteDate),
                    EstimatedPracticalCompletionDate = ParseExactDateTime(dto.EstimatedPracticalCompletionDate)
                };

                await _sender.Send(request);

                return RedirectToAction("Index");
            }

            // go back
            return RedirectToAction("Update", dto);
        }

        #endregion

        private const string DATE_FORMAT = "yyyy-MM-ddTHH:mm:ss";

        private static DateTime? ParseExactDateTime(string dateTimeString)
        {
            return DateTime.TryParseExact(
                dateTimeString,
                DATE_FORMAT,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var result) ? result : null;
        }
    }

    public class ManageProgrammeDto 
    {
        public int? PageNo { get; set; }
        public string Search { get; set; }

        public EFinancialYearFilter[] SelectedWic { get; set; } = [];
        public EFinancialYearFilter[] UnselectedWic { get; set; } = [];

        public EFinancialYearFilter[] SelectedSos { get; set; } = [];
        public EFinancialYearFilter[] UnselectedSos { get; set; } = [];

        public EFinancialYearFilter[] SelectedPc { get; set; } = [];
        public EFinancialYearFilter[] UnselectedPc { get; set; } = [];

        public EApplicationScheme[] SelectedScheme { get; set; } = [];
        public EApplicationScheme[] UnselectedScheme { get; set; } = [];

        public string[] Source { get; set; } = [];
        public bool ShowFilters { get; set; }
    }

    public class UpdateDto
    {
        public string EstimatedInvestigationCompletionDate { get; set; }
        public string EstimatedStartOnSiteDate { get; set; }
        public string EstimatedPracticalCompletionDate { get; set; }

        public string[] AppId { get; set; }
    }
}