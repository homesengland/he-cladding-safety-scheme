using AutoMapper;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.Application.ExistingApplication.GetExistingApplication;
using HE.Remediation.Core.UseCase.Areas.Application.NewApplication.CreateNewApplication;
using HE.Remediation.Core.UseCase.Areas.Application.NewApplication.SetExistingApplication;
using HE.Remediation.WebApp.Authorisation;
using HE.Remediation.WebApp.Helpers;
using HE.Remediation.WebApp.ViewModels;
using HE.Remediation.WebApp.ViewModels.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.Application.Controllers
{
    [Area("Application")]
    [CookieAuthorise]
    public class DashboardController : Controller
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public DashboardController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        #region "Dashboard"

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region "New Application"

        [HttpGet]
        public IActionResult NewApplication()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Start()
        {
            await _sender.Send(CreateNewApplicationRequest.Request);
            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        #endregion

        #region "Existing Application"

        public async Task<IActionResult> ExistingApplications(int? PageNo)
        {
            if (PageNo == null)
            {
                PageNo = 1;
            }

            var existingApplicationsResponse = await _sender.Send(GetExistingApplicationRequest.Request);
            var viewModel = new ExistingApplicationViewModel();

            var applications = _mapper.Map<List<ApplicationViewModel>>(existingApplicationsResponse);

            PagnationRangeValuesViewModel pagingRangeValuesViewModel = PaginationHelper.ObtainPageHandlingDetails(PageNo.Value, applications.Count,
                10, false);
            viewModel.CurrentPage = pagingRangeValuesViewModel.CurrentPage;
            viewModel.ApplicationList = applications.Skip(pagingRangeValuesViewModel.StartRecordValue).Take(10).ToList();
            viewModel.PageCount = pagingRangeValuesViewModel.NoOfPages;
            viewModel.UseEllipses = pagingRangeValuesViewModel.UseEllipses;
            return View(viewModel);
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
                return NotFound();
            }
        }

        #endregion

        #region "Get Help"

        public IActionResult GetHelp()
        {
            return View();
        }

        #endregion

    }
}