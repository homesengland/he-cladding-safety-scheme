using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.Application.Dashboard.AcknowledgeNotification;
using HE.Remediation.Core.UseCase.Areas.Application.Dashboard.GetPretender;
using HE.Remediation.Core.UseCase.Areas.Application.Dashboard.SchemeSelection;
using HE.Remediation.Core.UseCase.Areas.Application.Dashboard.Tasks;
using HE.Remediation.Core.UseCase.Areas.Application.ExistingApplication.GetExistingApplication;
using HE.Remediation.Core.UseCase.Areas.Application.NewApplication.CreateNewApplication;
using HE.Remediation.Core.UseCase.Areas.Application.NewApplication.SetExistingApplication;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.Attributes.Routing;
using HE.Remediation.WebApp.Helpers;
using HE.Remediation.WebApp.ViewModels.Application;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.Application.Controllers
{
    [Area("Application")]
    [CookieAuthorise]
    public class DashboardController : Controller
    {
        private const string TempDataKey_SelectedScheme = "SelectedScheme";
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public DashboardController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        #region "Dashboard"

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _sender.Send(GetPreTenderRequest.Request);

            var viewModel = new DashboardViewModel
            {
                AlertCount = response.AlertCount,
                NotificationMessage = response.NotificationMessage
            };

            return View(viewModel);
        }

        [HttpGet("AcknowledgedNotification/{id:guid}")]
        public async Task<IActionResult> AcknowledgedNotification([FromRoute] GetAcknowledgeNotificationRequest request)
        {
            await _sender.Send(request);

            return RedirectToAction("Index", "StageDiagram");
        }

        #endregion

        #region "Scheme Selection"

        [HttpGet("SchemeSelection")]
        public async Task<IActionResult> SchemeSelection()
        {
            var response = await _sender.Send(SchemeSelectionRequest.Request);

            var viewModel = new SchemeSelectionViewModel
            {
                Schemes = response.Schemes.ToList()
            };

            if (TempData.ContainsKey(TempDataKey_SelectedScheme))
            {
                viewModel.SelectedSchemeId = (int)(EApplicationScheme)TempData[TempDataKey_SelectedScheme];
                TempData.Keep(TempDataKey_SelectedScheme);
            }

            return View(viewModel);
        }

        [HttpPost("SchemeSelection")]
        public async Task<IActionResult> SchemeSelection(SchemeSelectionViewModel viewModel)
        {
            var response = await _sender.Send(SchemeSelectionRequest.Request);

            viewModel.Schemes = response.Schemes.ToList();

            var validator = new SchemeSelectionViewModelValidator();

            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, String.Empty);
                return View(viewModel);
            }

            TempData[TempDataKey_SelectedScheme] = (EApplicationScheme)viewModel.SelectedSchemeId;

            if (viewModel.SelectedSchemeId.Equals((int)EApplicationScheme.CladdingSafetyScheme))
            {
                return RedirectToAction(nameof(NewApplication));
            }
            else
            {
                return RedirectToAction(nameof(NewBuilding));
            }
        }

        #endregion

        #region "New Application"

        [HttpGet]
        public IActionResult NewApplication()
        {
            IfNoSchemeRedirectToSchemeSelection();
            TempData[TempDataKey_SelectedScheme] = TempData[TempDataKey_SelectedScheme];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Start(NewApplicationOrBuildingViewModel viewModel)
        {
            IfNoSchemeRedirectToSchemeSelection();

            TempData.TryGetValue(TempDataKey_SelectedScheme, out var scheme);

            var request = CreateNewApplicationRequest.Request;
            request.ApplicationScheme = (EApplicationScheme)scheme;
            await _sender.Send(request);

            TempData.Remove(TempDataKey_SelectedScheme);

            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        #endregion

        #region "New Building"

        [HttpGet]
        public IActionResult NewBuilding()
        {
            IfNoSchemeRedirectToSchemeSelection();
            TempData[TempDataKey_SelectedScheme] = TempData[TempDataKey_SelectedScheme];
            return View();
        }
        #endregion

        #region "Existing Application"

        public async Task<IActionResult> ExistingApplications(int? pageNo, string search, EApplicationStage[] selectedFilterStages, EApplicationStage[] unselectedFilterStages, string[] source, bool ShowFiltersValue)
        {
            const int pageSize = 10;
            pageNo ??= 1;

            var viewModel = new ExistingApplicationViewModel();
            IReadOnlyCollection<GetExistingApplicationResponse> existingApplicationsResponse = await FilterStages(search, selectedFilterStages, unselectedFilterStages, source, ShowFiltersValue, viewModel);

            var applications = _mapper.Map<IReadOnlyCollection<ExistingApplicationViewModel.ApplicationViewModel>>(existingApplicationsResponse);

            var pagingRangeValuesViewModel = PaginationHelper.ObtainPageHandlingDetails(pageNo.Value, applications.Count, pageSize, false);
            viewModel.CurrentPage = pagingRangeValuesViewModel.CurrentPage;
            viewModel.ApplicationList = applications.Skip(pagingRangeValuesViewModel.StartRecordValue).Take(pageSize).ToArray();
            viewModel.PageCount = pagingRangeValuesViewModel.NoOfPages;
            viewModel.UseEllipses = pagingRangeValuesViewModel.UseEllipses;
            return View(viewModel);
        }

        private async Task<IReadOnlyCollection<GetExistingApplicationResponse>> FilterStages(string search, EApplicationStage[] selectedFilterStages, EApplicationStage[] unselectedFilterStages, string[] source, bool ShowFiltersValue, ExistingApplicationViewModel viewModel)
        {
            viewModel.SelectedFilterStageOptions = selectedFilterStages;

            if (source.Contains("showFilters"))
            {
                viewModel.ShowFiltersValue = !ShowFiltersValue;
            }

            if (source.Contains("clear"))
            {
                viewModel.SelectedFilterStageOptions = [];
            }
            else if (source.Contains("unselect"))
            {
                viewModel.SelectedFilterStageOptions = selectedFilterStages.Except(unselectedFilterStages);
            }

            if (source.Contains("applyFilters"))
            {
                viewModel.ShowFiltersValue = false;
            }

            var existingApplicationsResponse = await _sender.Send(new GetExistingApplicationRequest
            {
                Search = search,
                SelectedFilterStageOptions = viewModel.SelectedFilterStageOptions
            });
            return existingApplicationsResponse;
        }

        [HttpGet(nameof(ExistingApplication))]

        public async Task<IActionResult> ExistingApplication(SetExistingApplicationRequest request)
        {
            try
            {
                await _sender.Send(request);
                return RedirectToAction("Index", "StageDiagram", new { area = "Application" });
            }
            catch (EntityNotFoundException)
            {
                throw new EntityNotFoundException("Existing application not found");
            }
        }

        #endregion

        #region "Get Help"

        public IActionResult GetHelp()
        {
            return View();
        }

        #endregion

        #region "Tasks"
        public async Task<IActionResult> Tasks()
        {
            var response = await _sender.Send(GetTasksRequest.Request);

            return View(response.Alerts);
        }
        #endregion

        public IActionResult Complaints()
        {
            return View();
        }

        public IActionResult Appeals()
        {
            return View();
        }

        private void IfNoSchemeRedirectToSchemeSelection()
        {
            TempData.TryGetValue(TempDataKey_SelectedScheme, out var scheme);

            if (scheme == null)
            {
                RedirectToAction(nameof(SchemeSelection));
            }
        }

    }
}