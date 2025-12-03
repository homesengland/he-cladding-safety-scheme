using System;
using FluentValidation.TestHelper;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
using Xunit;

namespace HE.Remediation.WebApp.Tests.Validators;

public class TellUsAboutPlanningPermissionViewModelValidatorTests
{
    private readonly TellUsAboutPlanningPermissionViewModelValidator _validator = new();

    #region Planning Permission Date Submitted Tests

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionDateSubmittedMonth_Is_Out_Of_Range()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = 13,
            PlanningPermissionDateSubmittedYear = 2024
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedMonth)
            .WithErrorMessage("Date Submitted - invalid month");
    }

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionDateSubmittedYear_Is_Out_Of_Range()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = 12,
            PlanningPermissionDateSubmittedYear = 1999
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedYear)
            .WithErrorMessage("Date Submitted - invalid year");
    }

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionDateSubmittedMonth_Is_Null_And_No_ApprovedDate()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = null,
            PlanningPermissionDateSubmittedYear = 2024,
            PlanningPermissionDateApprovedMonth = null,
            PlanningPermissionDateApprovedYear = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedMonth)
            .WithErrorMessage("Date Submitted - month required");
    }

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionDateSubmittedYear_Is_Null_And_No_ApprovedDate()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = 12,
            PlanningPermissionDateSubmittedYear = null,
            PlanningPermissionDateApprovedMonth = null,
            PlanningPermissionDateApprovedYear = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedYear)
            .WithErrorMessage("Date Submitted - year required");
    }

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionDateSubmitted_Is_In_Future()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = futureDate.Month,
            PlanningPermissionDateSubmittedYear = futureDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedYear)
            .WithErrorMessage("Date Submitted cannot be in the future");
    }

    [Fact]
    public void Should_Have_Error_When_PreviousPlanningPermissionDateSubmitted_Exists_But_Current_Is_Null()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PreviousPlanningPermissionDateSubmitted = DateTime.Now.AddMonths(-6),
            PlanningPermissionDateSubmittedMonth = null,
            PlanningPermissionDateSubmittedYear = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedYear)
            .WithErrorMessage("Date Submitted was supplied previously so must be kept");
    }

    [Fact]
    public void Should_Not_Have_Error_When_PlanningPermissionDateSubmitted_Is_Valid_Past_Date()
    {
        var pastDate = DateTime.Now.AddMonths(-6);
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = pastDate.Month,
            PlanningPermissionDateSubmittedYear = pastDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedMonth);
        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedYear);
    }

    [Fact]
    public void Should_Not_Have_Error_When_PlanningPermissionDateSubmitted_Not_Required_Because_ApprovedDate_Exists()
    {
        var pastDate = DateTime.Now.AddMonths(-3);
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = null,
            PlanningPermissionDateSubmittedYear = null,
            PlanningPermissionDateApprovedMonth = pastDate.Month,
            PlanningPermissionDateApprovedYear = pastDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedMonth);
        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedYear);
    }

    #endregion

    #region Planning Permission Date Approved Tests

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionDateApprovedMonth_Is_Null_But_Year_Is_Provided()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateApprovedMonth = null,
            PlanningPermissionDateApprovedYear = 2024
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedMonth)
            .WithErrorMessage("Date Approved - month required");
    }

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionDateApprovedYear_Is_Null_But_Month_Is_Provided()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateApprovedMonth = 12,
            PlanningPermissionDateApprovedYear = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedYear)
            .WithErrorMessage("Date Approved - year required");
    }

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionDateApprovedMonth_Is_Out_Of_Range()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateApprovedMonth = 0,
            PlanningPermissionDateApprovedYear = 2024
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedMonth)
            .WithErrorMessage("Date Approved - invalid month");
    }

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionDateApprovedYear_Is_Out_Of_Range()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateApprovedMonth = 12,
            PlanningPermissionDateApprovedYear = 3001
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedYear)
            .WithErrorMessage("Date Approved - invalid year");
    }

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionDateApproved_Is_In_Future()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateApprovedMonth = futureDate.Month,
            PlanningPermissionDateApprovedYear = futureDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedYear)
            .WithErrorMessage("Date Approved cannot be in the future");
    }

    [Fact]
    public void Should_Have_Error_When_PlanningPermissionDateApproved_Is_Before_DateSubmitted()
    {
        var submittedDate = DateTime.Now.AddMonths(-3);
        var approvedDate = DateTime.Now.AddMonths(-6);
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = submittedDate.Month,
            PlanningPermissionDateSubmittedYear = submittedDate.Year,
            PlanningPermissionDateApprovedMonth = approvedDate.Month,
            PlanningPermissionDateApprovedYear = approvedDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedYear)
            .WithErrorMessage("Date Approved cannot be before the Date Submitted");
    }

    [Fact]
    public void Should_Have_Error_When_PreviousPlanningPermissionDateApproved_Exists_But_Current_Is_Null()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PreviousPlanningPermissionDateApproved = DateTime.Now.AddMonths(-3),
            PlanningPermissionDateApprovedMonth = null,
            PlanningPermissionDateApprovedYear = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedYear)
            .WithErrorMessage("Date Approved was supplied previously so must be kept");
    }

    [Fact]
    public void Should_Not_Have_Error_When_PlanningPermissionDateApproved_Is_Valid_Past_Date()
    {
        var pastDate = DateTime.Now.AddMonths(-3);
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateApprovedMonth = pastDate.Month,
            PlanningPermissionDateApprovedYear = pastDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedMonth);
        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedYear);
    }

    [Fact]
    public void Should_Not_Have_Error_When_PlanningPermissionDateApproved_Is_Same_As_DateSubmitted()
    {
        var sameDate = DateTime.Now.AddMonths(-3);
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = sameDate.Month,
            PlanningPermissionDateSubmittedYear = sameDate.Year,
            PlanningPermissionDateApprovedMonth = sameDate.Month,
            PlanningPermissionDateApprovedYear = sameDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedYear);
    }

    [Fact]
    public void Should_Not_Have_Error_When_PlanningPermissionDateApproved_Is_After_DateSubmitted()
    {
        var submittedDate = DateTime.Now.AddMonths(-6);
        var approvedDate = DateTime.Now.AddMonths(-3);
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = submittedDate.Month,
            PlanningPermissionDateSubmittedYear = submittedDate.Year,
            PlanningPermissionDateApprovedMonth = approvedDate.Month,
            PlanningPermissionDateApprovedYear = approvedDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedMonth);
        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedYear);
    }

    #endregion

    #region Valid Scenarios Tests

    [Fact]
    public void Should_Not_Have_Error_When_Only_DateSubmitted_Is_Provided_And_Valid()
    {
        var pastDate = DateTime.Now.AddMonths(-6);
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = pastDate.Month,
            PlanningPermissionDateSubmittedYear = pastDate.Year,
            PlanningPermissionDateApprovedMonth = null,
            PlanningPermissionDateApprovedYear = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_Both_Dates_Are_Valid()
    {
        var submittedDate = DateTime.Now.AddMonths(-6);
        var approvedDate = DateTime.Now.AddMonths(-3);
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = submittedDate.Month,
            PlanningPermissionDateSubmittedYear = submittedDate.Year,
            PlanningPermissionDateApprovedMonth = approvedDate.Month,
            PlanningPermissionDateApprovedYear = approvedDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_Only_DateApproved_Is_Provided_And_Valid()
    {
        var pastDate = DateTime.Now.AddMonths(-3);
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = null,
            PlanningPermissionDateSubmittedYear = null,
            PlanningPermissionDateApprovedMonth = pastDate.Month,
            PlanningPermissionDateApprovedYear = pastDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    #endregion

    #region Edge Cases Tests

    [Fact]
    public void Should_Have_Error_When_DateSubmitted_Month_Is_Negative()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = -1,
            PlanningPermissionDateSubmittedYear = 2024
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedMonth)
            .WithErrorMessage("Date Submitted - invalid month");
    }

    [Fact]
    public void Should_Have_Error_When_DateApproved_Month_Is_Greater_Than_12()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateApprovedMonth = 13,
            PlanningPermissionDateApprovedYear = 2024
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedMonth)
            .WithErrorMessage("Date Approved - invalid month");
    }

    [Fact]
    public void Should_Have_Error_When_DateSubmitted_Year_Is_Below_Minimum()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = 12,
            PlanningPermissionDateSubmittedYear = 1999
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedYear)
            .WithErrorMessage("Date Submitted - invalid year");
    }

    [Fact]
    public void Should_Have_Error_When_DateApproved_Year_Is_Above_Maximum()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateApprovedMonth = 12,
            PlanningPermissionDateApprovedYear = 3001
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedYear)
            .WithErrorMessage("Date Approved - invalid year");
    }

    [Fact]
    public void Should_Not_Have_Error_When_DateSubmitted_Is_Current_Month()
    {
        var currentDate = DateTime.Now;
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = currentDate.Month,
            PlanningPermissionDateSubmittedYear = currentDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedMonth);
        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedYear);
    }

    [Fact]
    public void Should_Not_Have_Error_When_DateApproved_Is_Current_Month()
    {
        var currentDate = DateTime.Now;
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateApprovedMonth = currentDate.Month,
            PlanningPermissionDateApprovedYear = currentDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedMonth);
        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateApprovedYear);
    }

    #endregion

    #region Boundary Tests

    [Fact]
    public void Should_Not_Have_Error_When_DateSubmitted_Month_Is_1()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = 1,
            PlanningPermissionDateSubmittedYear = 2024
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedMonth);
    }

    [Fact]
    public void Should_Not_Have_Error_When_DateSubmitted_Month_Is_12()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = 12,
            PlanningPermissionDateSubmittedYear = 2024
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedMonth);
    }

    [Fact]
    public void Should_Not_Have_Error_When_DateSubmitted_Year_Is_2000()
    {
        var model = new TellUsAboutPlanningPermissionViewModel
        {
            PlanningPermissionDateSubmittedMonth = 12,
            PlanningPermissionDateSubmittedYear = 2000
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PlanningPermissionDateSubmittedYear);
    }

    #endregion
}