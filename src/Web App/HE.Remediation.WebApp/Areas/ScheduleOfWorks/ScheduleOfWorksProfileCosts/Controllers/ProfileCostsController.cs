using AutoMapper;
using FluentValidation.AspNetCore;
using HE.Remediation.Core.Enums;
using FluentValidation.Results;
using HE.Remediation.Core.Helpers;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BaseInformation.Get;
using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;
using HE.Remediation.WebApp.ViewModels.Shared;
using HE.Remediation.Core.UseCase.Shared.Costs.Get;
using HE.Remediation.Core.UseCase.Shared.Costs.Set;
using MediatR;

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ProjectDates.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ProjectDates.Set;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Costs.Create;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.CheckYourAnswers.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.CheckYourAnswers.Set;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ConfirmChangeProjectDates.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Costs.Delete;
using HE.Remediation.WebApp.Attributes.Routing;

namespace HE.Remediation.WebApp.Areas.ScheduleOfWorks.ScheduleOfWorksProfileCosts.Controllers
{

    [Area("ScheduleOfWorksProfileCosts")]
    [Route("ScheduleOfWorks/ProfileCosts")]
    public class ProfileCostsController : StartController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public ProfileCostsController(ISender sender, IMapper mapper) : base(sender)
        {
            _sender = sender;
            _mapper = mapper;
        }
        protected override IActionResult DefaultStart =>
         RedirectToAction("StartInformation", "ProfileCosts", new { Area = "ScheduleOfWorksProfileCosts" });

        #region Start Information

        [HttpGet(nameof(StartInformation))]
        public async Task<IActionResult> StartInformation()
        {
            var response = await _sender.Send(GetBaseInformationRequest.Request);
            var viewModel = _mapper.Map<ProfileCostsStartInformationViewModel>(response);

            viewModel.ReturnUrl = string.Empty;
            return View(viewModel);
        }

        [HttpPost(nameof(StartInformation))]
        public IActionResult StartInformation(StartInformationViewModel viewModel)
        {
            return RedirectToAction("ProjectDates", "ProfileCosts", new { Area = "ScheduleOfWorksProfileCosts" });
        }

        #endregion

        #region "What are your project start and end dates?"

        [HttpGet(nameof(ProjectDates))]
        public async Task<IActionResult> ProjectDates(string returnUrl)
        {
            var response = await _sender.Send(GetProjectDatesRequest.Request);
            var viewModel = _mapper.Map<ProjectDatesViewModel>(response);

            if (viewModel.ProjectStartDateMonth.HasValue && viewModel.ProjectStartDateYear.HasValue)
            {
                var validator = new ProjectDatesViewModelValidator();
                var validationResult = await validator.ValidateAsync(viewModel);

                if (!validationResult.IsValid)
                {
                    validationResult.AddToModelState(ModelState, string.Empty);
                }
            }

            viewModel.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        [HttpPost(nameof(ProjectDates))]
        public async Task<IActionResult> ProjectDates(ProjectDatesViewModel viewModel)
        {
            var validator = new ProjectDatesViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            var willChangeCostsProfile = await WillProjectDatesChangeAffectCostsProfile(viewModel);
            if (willChangeCostsProfile)
            {
                var projectDatesJson = JsonSerializer.Serialize(viewModel);
                TempData["ProjectDates"] = projectDatesJson;
                return viewModel.ReturnUrl is not null
                    ? RedirectToAction("ConfirmChangeProjectDates", "ProfileCosts", new { Area = "ScheduleOfWorksProfileCosts", returnUrl = viewModel.ReturnUrl })
                    : RedirectToAction("ConfirmChangeProjectDates", "ProfileCosts", new { Area = "ScheduleOfWorksProfileCosts" });
            }

            var request = _mapper.Map<SetProjectDatesRequest>(viewModel);
            await _sender.Send(request);

            if (viewModel.ReturnUrl is not null)
            {
                return RedirectToAction(viewModel.ReturnUrl, "ProfileCosts", new { Area = "ScheduleOfWorksProfileCosts" });
            }

            return viewModel.SubmitAction == ESubmitAction.Continue
                ? RedirectToAction("Milestones", "ProfileCosts", new { Area = "ScheduleOfWorksProfileCosts" })
                : RedirectToAction("TaskList", "ScheduleOfWorks", new { Area = "ScheduleOfWorks" });
        }

        private async Task<bool> WillProjectDatesChangeAffectCostsProfile(ProjectDatesViewModel viewModel)
        {
            var existingCosts = await _sender.Send(GetCostsRequest.Request);
            var previousProjectDates = await _sender.Send(GetProjectDatesRequest.Request);

            return existingCosts is not null &&
                   existingCosts.MonthlyCosts != null &&
                   existingCosts.MonthlyCosts.Any() &&
                   previousProjectDates is not null &&
                   (previousProjectDates.ProjectStartDateMonth != viewModel.ProjectStartDateMonth ||
                    previousProjectDates.ProjectStartDateYear != viewModel.ProjectStartDateYear ||
                    previousProjectDates.ProjectEndDateMonth != viewModel.ProjectEndDateMonth ||
                    previousProjectDates.ProjectEndDateYear != viewModel.ProjectEndDateYear);
        }

        #endregion

        #region "Profile your schedule of works"

        [HttpGet(nameof(Milestones))]
        public async Task<IActionResult> Milestones(string returnUrl)
        {
            var response = await _sender.Send(GetCostsRequest.Request);
            var viewModel = _mapper.Map<MilestonesViewModel>(response);

            viewModel.ReturnUrl = returnUrl;
            if (viewModel.Costs is not null) viewModel.Costs.IsPaymentRequest = false;
            return View(viewModel);
        }

        [HttpPost(nameof(Milestones))]
        public async Task<IActionResult> Milestones(MilestonesViewModel viewModel)
        {
            var validator = new MilestonesViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            UpdateViewModelCosts(viewModel.Costs, validationResult);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                return View(viewModel);
            }

            var request = _mapper.Map<SetCostsRequest>(viewModel.Costs);
            await _sender.Send(request);

            return viewModel.SubmitAction == ESubmitAction.Continue
                ? RedirectToAction("PaymentsSummary", "ProfileCosts", new { Area = "ScheduleOfWorksProfileCosts" })
                : RedirectToAction("TaskList", "ScheduleOfWorks", new { area = "ScheduleOfWorks" });
        }

        [HttpPost(nameof(RecalculateMilestones))]
        public async Task<IActionResult> RecalculateMilestones(MilestonesViewModel viewModel)
        {
            var validator = new MilestonesViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
            }

            UpdateViewModelCosts(viewModel.Costs, validationResult);

            return View("Milestones", viewModel);
        }

        private void UpdateViewModelCosts(CostsViewModel viewModel, ValidationResult validationResult)
        {
            if (viewModel is null) return;

            var calculatedCosts = CostsCalculationHelper.CalculateMonthlyCosts(new MonthlyCostsCalculationRequest
            {
                ApprovedGrantFunding = viewModel.ApprovedGrantFunding,
                GrantPaidToDate = viewModel.GrantPaidToDate,
                MonthlyCosts = viewModel.MonthlyCosts.Select(x => x.Amount ?? 0)
            });

            viewModel.MonthlyCostsTotal = calculatedCosts.TotalMonthlyCosts;
            viewModel.UnprofiledGrantFunding = calculatedCosts.UnprofiledAmount;

            var validationErrors = validationResult.ToDictionary();
            for (var i = 0; i < viewModel.MonthlyCosts.Count; i++)
            {
                var costItem = viewModel.MonthlyCosts[i];
                if (!validationErrors.TryGetValue($"Costs.MonthlyCosts[{i}].AmountText", out _))
                {
                    costItem.AmountText = costItem.Amount?.ToString("N0");
                }
            }
        }

        #endregion

        #region "Review your schedule of works"

        [HttpGet(nameof(PaymentsSummary))]
        public async Task<IActionResult> PaymentsSummary()
        {
            var response = await _sender.Send(GetCostsRequest.Request);
            var viewModel = _mapper.Map<PaymentsSummaryViewModel>(response);

            viewModel.ReturnUrl = string.Empty;
            if (viewModel.Costs is not null) viewModel.Costs.IsPaymentRequest = false;
            return View(viewModel);
        }

        [HttpPost(nameof(PaymentsSummary))]
        public async Task<IActionResult> PaymentsSummary(PaymentsSummaryViewModel viewModel)
        {
            var validator = new PaymentsSummaryViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState, string.Empty);
                var response = await _sender.Send(GetCostsRequest.Request);
                viewModel = _mapper.Map<PaymentsSummaryViewModel>(response);
                return View(viewModel);
            }

            return viewModel.SubmitAction == ESubmitAction.Continue
                ? RedirectToAction("CheckYourAnswers", "ProfileCosts", new { Area = "ScheduleOfWorksProfileCosts" })
                : RedirectToAction("TaskList", "ScheduleOfWorks", new { area = "ScheduleOfWorksProfileCosts" });
        }

        #endregion

        #region "Confirm change project dates"

        [ExcludeRouteRecording]
        [HttpGet(nameof(ConfirmChangeProjectDates))]
        public async Task<IActionResult> ConfirmChangeProjectDates(string returnUrl)
        {
            var projectDatesJson = TempData["ProjectDates"]?.ToString();
            var projectDates = projectDatesJson != null
                ? JsonSerializer.Deserialize<ProjectDatesViewModel>(projectDatesJson)
                : null;

            var response = await _sender.Send(GetConfirmChangeProjectDatesRequest.Request);
            var viewModel = _mapper.Map<ConfirmChangeProjectDatesViewModel>(response);
            if (projectDates is not null)
            {
                _mapper.Map(projectDates, viewModel);
            }

            viewModel.ReturnUrl = returnUrl;
            return View(viewModel);
        }

        [HttpPost(nameof(ConfirmChangeProjectDates))]
        public async Task<IActionResult> ConfirmChangeProjectDates(ConfirmChangeProjectDatesViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (viewModel.Proceed == false)
            {
                return viewModel.ReturnUrl is not null
                    ? RedirectToAction("ProjectDates", "ProfileCosts", new { Area = "ScheduleOfWorksProfileCosts", returnUrl = viewModel.ReturnUrl })
                    : RedirectToAction("ProjectDates", "ProfileCosts", new { Area = "ScheduleOfWorksProfileCosts" });
            }

            await _sender.Send(DeleteCostsRequest.Request);

            var request = _mapper.Map<SetProjectDatesRequest>(viewModel);
            await _sender.Send(request);

            return viewModel.ReturnUrl is not null
                ? RedirectToAction("StartInformation", "ProfileCosts", new { Area = "ScheduleOfWorksProfileCosts", returnUrl = viewModel.ReturnUrl })
                : RedirectToAction("StartInformation", "ProfileCosts", new { Area = "ScheduleOfWorksProfileCosts" });
        }
        #endregion

        #region "Check your answers"

        [HttpGet(nameof(CheckYourAnswers))]
        public async Task<IActionResult> CheckYourAnswers()
        {
            var response = await _sender.Send(GetCheckYourAnswersRequest.Request);
            var viewModel = _mapper.Map<CheckYourAnswersViewModel>(response);

            viewModel.ReturnUrl = string.Empty;
            return View(viewModel);
        }

        [HttpPost(nameof(CheckYourAnswers))]
        public async Task<IActionResult> CheckYourAnswers(CheckYourAnswersViewModel viewModel)
        {
            await _sender.Send(SetCheckYourAnswersRequest.Request);
            return RedirectToAction("TaskList", "ScheduleOfWorks", new { area = "ScheduleOfWorksProfileCosts" });
        }

        #endregion
    }
}