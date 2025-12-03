using FluentValidation.TestHelper;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

namespace HE.Remediation.WebApp.Tests.Validators;

public class KeyDatesViewModelValidatorTests
{
    private readonly KeyDatesViewModelValidator _validator = new();

    #region Works Planning Tests

    [Fact]
    public void Should_Have_Error_When_No_Tender_Dates_Are_Provided()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = null,
            ActualTenderDate = null,
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ActualLeadContractAppointmentDate = null,
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("WorksPlanning")
            .WithErrorMessage("Please enter either an expected and/or actual Date of Tender");
    }

    [Fact]
    public void Should_Have_Error_When_No_Lead_Contractor_Appointment_Dates_Are_Provided()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ActualTenderDate = null,
            ExpectedLeadContractorAppointmentDate = null,
            ActualLeadContractAppointmentDate = null,
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("WorksPlanning")
            .WithErrorMessage("Please enter either an expected and/or actual Lead Contractor Appointment Date");
    }

    [Fact]
    public void Should_Have_Error_When_WorksPlanningDatesHaveChanged_But_No_ChangeType()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            PreviousExpectedTenderDate = DateTime.Now.AddMonths(2),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            WorksPlanningChangeType = null,
            WorksPlanningChangeReason = "Some reason",
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("WorksPlanning")
            .WithErrorMessage("Please enter why works planning dates have slipped");
    }

    [Fact]
    public void Should_Have_Error_When_WorksPlanningDatesHaveChanged_But_No_ChangeReason()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            PreviousExpectedTenderDate = DateTime.Now.AddMonths(2),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            WorksPlanningChangeType = "Delay",
            WorksPlanningChangeReason = null,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("WorksPlanning")
            .WithErrorMessage("Please enter additional details about why works planning dates have slipped");
    }

    [Fact]
    public void Should_Have_Error_When_ContractorTenderTypeId_Is_Not_Provided()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = null,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("WorksPlanning")
            .WithErrorMessage("Please enter how you are obtaining your contractor");
    }

    [Fact]
    public void Should_Have_Error_When_NonCompetitive_Tender_But_No_Reason()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.NonCompetitive,
            ContractorTenderReason = null,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("WorksPlanning")
            .WithErrorMessage("Please enter the reason you are not running a competitive tender to obtain your contractor");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Works_Planning_Is_Valid()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor("WorksPlanning");
    }

    #endregion

    #region Building Control Tests

    [Fact]
    public void Should_Have_Error_When_BuildingControlExpectedApplicationDate_Is_Not_Provided()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = null,
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("BuildingControl")
            .WithErrorMessage("Please enter either an expected and/or actual Building Control Expected Application Date");
    }

    [Fact]
    public void Should_Allow_When_BuildingControlExpectedApplicationDate_Is_Not_Provided_But_Actual_Is()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = null,
            BuildingControlActualApplicationDate = DateTime.Now,
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor("BuildingControl");
    }

    [Fact]
    public void Should_Have_Error_When_BuildingControlDecisionDate_Exists_But_No_Decision()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            BuildingControlDecisionDate = DateTime.Now.AddMonths(-1),
            BuildingControlDecision = null,
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("BuildingControl")
            .WithErrorMessage("Provide a Building Control Decision");
    }

    [Fact]
    public void Should_Have_Error_When_ApproveWithRecommendations_But_No_DecisionFiles()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            BuildingControlDecisionDate = DateTime.Now.AddMonths(-1),
            BuildingControlDecision = EBuildingControlDecisionType.ApproveWithRecommendations,
            BuildingControlDecisionFiles = new List<string>(),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("BuildingControl")
            .WithErrorMessage("Provide at least 1 document to support the building control decision");
    }

    [Fact]
    public void Should_Have_Error_When_Rejected_But_No_DecisionFiles()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            BuildingControlDecisionDate = DateTime.Now.AddMonths(-1),
            BuildingControlDecision = EBuildingControlDecisionType.Rejected,
            BuildingControlDecisionFiles = new List<string>(),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("BuildingControl")
            .WithErrorMessage("Provide at least 1 document to support the building control decision");
    }

    [Fact]
    public void Should_Have_Error_When_BuildingControlDatesHaveChanged_But_No_ChangeType()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(4),
            PreviousBuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            BuildingControlChangeType = null,
            BuildingControlChangeReason = "Some reason",
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("BuildingControl")
            .WithErrorMessage("Please enter a reason why building control dates have slipped");
    }

    [Fact]
    public void Should_Have_Error_When_BuildingControlDatesHaveChanged_But_No_ChangeReason()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(4),
            PreviousBuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            BuildingControlChangeType = "Delay",
            BuildingControlChangeReason = null,
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("BuildingControl")
            .WithErrorMessage("Please enter additional details about why building control dates have slipped");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Approved_Decision_Without_Files()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            BuildingControlDecisionDate = DateTime.Now.AddMonths(-1),
            BuildingControlDecision = EBuildingControlDecisionType.Approved,
            BuildingControlDecisionFiles = new List<string>(),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor("BuildingControl");
    }

    #endregion

    #region Planning Permission Tests

    [Fact]
    public void Should_Have_Error_When_WorksNeedPlanningPermission_Is_Not_Provided()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            WorksNeedPlanningPermission = null,
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("PlanningPermission")
            .WithErrorMessage("Please enter indicate if planning permission is required");
    }

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionRequired_And_Applied_But_No_SubmissionDate()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            WorksNeedPlanningPermission = 1,
            HaveAppliedPlanningPermission = true,
            PlanningPermissionDateSubmitted = null,
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("PlanningPermission")
            .WithErrorMessage("Please enter a Submission Date");
    }

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionRequired_And_NotApplied_But_No_Reason()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            WorksNeedPlanningPermission = 1,
            HaveAppliedPlanningPermission = false,
            PlanningPermissionReasonNotApplied = null,
            PlanningPermissionPlanToSubmitDate = DateTime.Now.AddMonths(2),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("PlanningPermission")
            .WithErrorMessage("Please enter a reason why planning permission is not required");
    }

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionRequired_And_NotApplied_But_No_PlanToSubmitDate()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            WorksNeedPlanningPermission = 1,
            HaveAppliedPlanningPermission = false,
            PlanningPermissionReasonNotApplied = "Not needed for this type of work",
            PlanningPermissionPlanToSubmitDate = null,
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("PlanningPermission")
            .WithErrorMessage("Please enter a date you plan to submit for planning permission");
    }

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionDatesHaveChanged_But_No_ChangeType()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            WorksNeedPlanningPermission = 1,
            HaveAppliedPlanningPermission = true,
            PlanningPermissionDateSubmitted = DateTime.Now.AddMonths(-2),
            PreviousPlanningPermissionDateSubmitted = DateTime.Now.AddMonths(-3),
            PlanningPermissionChangeType = null,
            PlanningPermissionChangeReason = "Some reason",
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("PlanningPermission")
            .WithErrorMessage("Please enter a reason why planning permission dates have slipped");
    }

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionDatesHaveChanged_But_No_ChangeReason()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            WorksNeedPlanningPermission = 1,
            HaveAppliedPlanningPermission = true,
            PlanningPermissionDateSubmitted = DateTime.Now.AddMonths(-2),
            PreviousPlanningPermissionDateSubmitted = DateTime.Now.AddMonths(-3),
            PlanningPermissionChangeType = "Delay",
            PlanningPermissionChangeReason = null,
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("PlanningPermission")
            .WithErrorMessage("Please enter additional details about why planning permission dates have slipped");
    }

    [Fact]
    public void Should_Not_Have_Error_When_PlanningPermission_Not_Required()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            WorksNeedPlanningPermission = 0,
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor("PlanningPermission");
    }

    #endregion

    #region Remediation Tests

    [Fact]
    public void Should_Have_Error_When_RemediationFullCompletionOfWorksDate_Is_Not_Provided()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            RemediationFullCompletionOfWorksDate = null,
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("Remediation")
            .WithErrorMessage("Please enter an expected remediation start date");
    }

    [Fact]
    public void Should_Have_Error_When_RemediationPracticalCompletionDate_Is_Not_Provided()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("Remediation")
            .WithErrorMessage("Please enter an expected practical completion date");
    }

    [Fact]
    public void Should_Have_Error_When_RemediationDatesHaveChanged_But_No_ChangeType()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(13),
            PreviousRemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18),
            RemediationChangeType = null,
            RemediationChangeReason = "Some reason"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("Remediation")
            .WithErrorMessage("Please enter a reason why remediation dates have slipped");
    }

    [Fact]
    public void Should_Have_Error_When_RemediationDatesHaveChanged_But_No_ChangeReason()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(13),
            PreviousRemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18),
            RemediationChangeType = "Delay",
            RemediationChangeReason = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("Remediation")
            .WithErrorMessage("Please enter additional details about why remediation dates have slipped");
    }

    [Fact]
    public void Should_Have_Error_When_Remediation_Dates_Do_Not_Come_Last()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(9), // After remediation dates
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(6), // Before building control
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(8) // Before building control
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor("Remediation")
            .WithErrorMessage("Remediation dates must be after all dates provided in other Key Dates sections.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Remediation_Dates_Come_Last()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor("Remediation");
    }

    #endregion

    #region Valid Scenarios Tests

    [Fact]
    public void Should_Not_Have_Error_When_All_Required_Fields_Are_Valid()
    {
        var model = new KeyDatesViewModel
        {
            // Works Planning
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            
            // Building Control
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            
            // Planning Permission
            WorksNeedPlanningPermission = 0, // No planning permission needed
            
            // Remediation
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_NonCompetitive_Tender_Has_Reason()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.NonCompetitive,
            ContractorTenderReason = "Single contractor with specific expertise",
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            WorksNeedPlanningPermission = 0,
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_Planning_Permission_Applied_With_Valid_Data()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            WorksNeedPlanningPermission = 1,
            HaveAppliedPlanningPermission = true,
            PlanningPermissionDateSubmitted = DateTime.Now.AddMonths(-2),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_Planning_Permission_Not_Applied_With_Valid_Data()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            WorksNeedPlanningPermission = 1,
            HaveAppliedPlanningPermission = false,
            PlanningPermissionReasonNotApplied = "Minor alterations that don't require permission",
            PlanningPermissionPlanToSubmitDate = DateTime.Now.AddMonths(2),
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_Building_Control_Has_Decision_Files_For_ApproveWithRecommendations()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            BuildingControlDecisionDate = DateTime.Now.AddMonths(-1),
            BuildingControlDecision = EBuildingControlDecisionType.ApproveWithRecommendations,
            BuildingControlDecisionFiles = new List<string> { "file1.pdf", "file2.pdf" },
            WorksNeedPlanningPermission = 0,
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    #endregion

    #region Edge Cases Tests

    [Fact]
    public void Should_Not_Have_Error_When_Using_Actual_Tender_Date_Instead_Of_Expected()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = null,
            ActualTenderDate = DateTime.Now.AddMonths(-1),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            WorksNeedPlanningPermission = 0,
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor("WorksPlanning");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Using_Actual_Lead_Contractor_Date_Instead_Of_Expected()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = null,
            ActualLeadContractAppointmentDate = DateTime.Now.AddMonths(-1),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            WorksNeedPlanningPermission = 0,
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor("WorksPlanning");
    }

    [Fact]
    public void Should_Not_Have_Error_When_WorksNeedPlanningPermission_Is_DontKnow()
    {
        var model = new KeyDatesViewModel
        {
            ExpectedTenderDate = DateTime.Now.AddMonths(3),
            ExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(6),
            ContractorTenderTypeId = EContractorTenderType.Competitive,
            BuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(3),
            WorksNeedPlanningPermission = 2, // Don't Know
            RemediationFullCompletionOfWorksDate = DateTime.Now.AddMonths(12),
            RemediationPracticalCompletionDate = DateTime.Now.AddMonths(18)
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor("PlanningPermission");
    }

    #endregion
}