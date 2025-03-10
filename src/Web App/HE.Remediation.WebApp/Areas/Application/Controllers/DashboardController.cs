﻿using AutoMapper;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.Application.Dashboard.AcknowledgeNotification;
using HE.Remediation.Core.UseCase.Areas.Application.Dashboard.GetPretender;
using HE.Remediation.Core.UseCase.Areas.Application.Dashboard.Tasks;
using HE.Remediation.Core.UseCase.Areas.Application.ExistingApplication.GetExistingApplication;
using HE.Remediation.Core.UseCase.Areas.Application.NewApplication.CreateNewApplication;
using HE.Remediation.Core.UseCase.Areas.Application.NewApplication.SetExistingApplication;
using HE.Remediation.WebApp.Attributes.Authorisation;
using HE.Remediation.WebApp.Helpers;
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

        public async Task<IActionResult> ExistingApplications(int? pageNo, string search)
        {
            const int pageSize = 10;
            pageNo ??= 1;

            var existingApplicationsResponse = await _sender.Send(new GetExistingApplicationRequest
            {
                Search = search
            });
            var viewModel = new ExistingApplicationViewModel();

            var applications = _mapper.Map<IReadOnlyCollection<ExistingApplicationViewModel.ApplicationViewModel>>(existingApplicationsResponse);

            var pagingRangeValuesViewModel = PaginationHelper.ObtainPageHandlingDetails(pageNo.Value, applications.Count, pageSize, false);
            viewModel.CurrentPage = pagingRangeValuesViewModel.CurrentPage;
            viewModel.ApplicationList = applications.Skip(pagingRangeValuesViewModel.StartRecordValue).Take(pageSize).ToArray();
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

    }
}