using System;
using FluentValidation.TestHelper;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
using Xunit;

namespace HE.Remediation.WebApp.Tests.Validators;

public class WorksPlanningViewModelValidatorTests
{
    private readonly WorksPlanningViewModelValidator _validator = new();

    #region Expected Tender Date Tests

    [Fact]
    public void Should_Have_Error_When_ExpectedTenderMonth_Is_Null_And_No_ActualTenderDate()
    {
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = null,
            ExpectedTenderYear = 2025,
            ActualTenderMonth = null,
            ActualTenderYear = null,
            ExpectedLeadContractorAppointmentMonth = 12,
            ExpectedLeadContractorAppointmentYear = 2025,
            ExpectedWorksPackageSubmissionMonth = 12,
            ExpectedWorksPackageSubmissionYear = 2025
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedTenderMonth)
            .WithErrorMessage("Expected Tender Date - month required");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedTenderYear_Is_Null_And_No_ActualTenderDate()
    {
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = 12,
            ExpectedTenderYear = null,
            ActualTenderMonth = null,
            ActualTenderYear = null,
            ExpectedLeadContractorAppointmentMonth = 12,
            ExpectedLeadContractorAppointmentYear = 2025,
            ExpectedWorksPackageSubmissionMonth = 12,
            ExpectedWorksPackageSubmissionYear = 2025
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedTenderYear)
            .WithErrorMessage("Expected Tender Date - year required");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedTenderMonth_Is_Out_Of_Range()
    {
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = 13,
            ExpectedTenderYear = 2025
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedTenderMonth)
            .WithErrorMessage("Expected Tender Date - invalid month");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedTenderYear_Is_Out_Of_Range()
    {
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = 12,
            ExpectedTenderYear = 1999
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedTenderYear)
            .WithErrorMessage("Expected Tender Date - invalid year");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedTenderDate_Is_In_Past()
    {
        var pastDate = DateTime.Now.AddMonths(-6);
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = pastDate.Month,
            ExpectedTenderYear = pastDate.Year,
            PreviousExpectedTenderDate = DateTime.Now.AddMonths(-12)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedTenderYear)
            .WithErrorMessage("Expected Tender Date must be in the future");
    }

    [Fact]
    public void Should_Have_Error_When_PreviousExpectedTenderDate_Exists_But_Current_Is_Null()
    {
        var model = new WorksPlanningViewModel
        {
            PreviousExpectedTenderDate = DateTime.Now.AddDays(30),
            ExpectedTenderMonth = null,
            ExpectedTenderYear = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedTenderYear)
            .WithErrorMessage("Actual Tender Date was supplied previously so must be kept");
    }

    [Fact]
    public void Should_Not_Have_Error_When_ExpectedTenderDate_Is_Valid_And_In_Future()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = futureDate.Month,
            ExpectedTenderYear = futureDate.Year,
            ExpectedLeadContractorAppointmentMonth = futureDate.Month,
            ExpectedLeadContractorAppointmentYear = futureDate.Year,
            ExpectedWorksPackageSubmissionMonth = futureDate.Month,
            ExpectedWorksPackageSubmissionYear = futureDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.ExpectedTenderMonth);
        result.ShouldNotHaveValidationErrorFor(x => x.ExpectedTenderYear);
    }

    #endregion

    #region Actual Tender Date Tests

    [Fact]
    public void Should_Have_Error_When_ActualTenderMonth_Is_Null_But_Year_Is_Provided()
    {
        var model = new WorksPlanningViewModel
        {
            ActualTenderMonth = null,
            ActualTenderYear = 2024
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ActualTenderMonth)
            .WithErrorMessage("Actual Tender Date month - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ActualTenderYear_Is_Null_But_Month_Is_Provided()
    {
        var model = new WorksPlanningViewModel
        {
            ActualTenderMonth = 12,
            ActualTenderYear = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ActualTenderYear)
            .WithErrorMessage("Actual Tender Date year - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ActualTenderMonth_Is_Out_Of_Range()
    {
        var model = new WorksPlanningViewModel
        {
            ActualTenderMonth = 0,
            ActualTenderYear = 2024
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ActualTenderMonth)
            .WithErrorMessage("Actual Tender Date month - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ActualTenderYear_Is_Out_Of_Range()
    {
        var model = new WorksPlanningViewModel
        {
            ActualTenderMonth = 12,
            ActualTenderYear = 3001
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ActualTenderYear)
            .WithErrorMessage("Actual Tender Date year - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ActualTenderDate_Is_In_Future()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new WorksPlanningViewModel
        {
            ActualTenderMonth = futureDate.Month,
            ActualTenderYear = futureDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedTenderYear)
            .WithErrorMessage("Actual Tender Date cannot be in the future");
    }

    [Fact]
    public void Should_Have_Error_When_ActualTenderDate_Is_Before_ExpectedTenderDate()
    {
        var expectedDate = DateTime.Now.AddMonths(-2);
        var actualDate = DateTime.Now.AddMonths(-3);
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = expectedDate.Month,
            ExpectedTenderYear = expectedDate.Year,
            ActualTenderMonth = actualDate.Month,
            ActualTenderYear = actualDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ActualTenderYear)
            .WithErrorMessage("Actual Tender Date cannot be before Expected Tender Date");
    }

    [Fact]
    public void Should_Have_Error_When_PreviousActualTenderDate_Exists_But_Current_Is_Null()
    {
        var model = new WorksPlanningViewModel
        {
            PreviousActualTenderDate = DateTime.Now.AddDays(-30),
            ActualTenderMonth = null,
            ActualTenderYear = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedTenderYear)
            .WithErrorMessage("Actual Tender Date was supplied previously so must be kept");
    }

    #endregion

    #region Expected Lead Contractor Appointment Date Tests

    [Fact]
    public void Should_Have_Error_When_ExpectedLeadContractorAppointmentMonth_Is_Null_And_No_ActualDate()
    {
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = 12,
            ExpectedTenderYear = 2025,
            ExpectedLeadContractorAppointmentMonth = null,
            ExpectedLeadContractorAppointmentYear = 2025,
            ActualLeadContractorAppointmentMonth = null,
            ActualLeadContractorAppointmentYear = null,
            ExpectedWorksPackageSubmissionMonth = 12,
            ExpectedWorksPackageSubmissionYear = 2025
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedLeadContractorAppointmentMonth)
            .WithErrorMessage("Expected Lead Contractor Appointment month - required");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedLeadContractorAppointmentYear_Is_Null_And_No_ActualDate()
    {
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = 12,
            ExpectedTenderYear = 2025,
            ExpectedLeadContractorAppointmentMonth = 12,
            ExpectedLeadContractorAppointmentYear = null,
            ActualLeadContractorAppointmentMonth = null,
            ActualLeadContractorAppointmentYear = null,
            ExpectedWorksPackageSubmissionMonth = 12,
            ExpectedWorksPackageSubmissionYear = 2025
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedLeadContractorAppointmentYear)
            .WithErrorMessage("Expected Lead Contractor Appointment year - required");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedLeadContractorAppointmentMonth_Is_Out_Of_Range()
    {
        var model = new WorksPlanningViewModel
        {
            ExpectedLeadContractorAppointmentMonth = 13,
            ExpectedLeadContractorAppointmentYear = 2025
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedLeadContractorAppointmentMonth)
            .WithErrorMessage("Expected Lead Contractor Appointment month - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedLeadContractorAppointmentYear_Is_Out_Of_Range()
    {
        var model = new WorksPlanningViewModel
        {
            ExpectedLeadContractorAppointmentMonth = 12,
            ExpectedLeadContractorAppointmentYear = 1999
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedLeadContractorAppointmentYear)
            .WithErrorMessage("Expected Lead Contractor Appointment year - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedLeadContractorAppointmentDate_Is_In_Past()
    {
        var pastDate = DateTime.Now.AddMonths(-6);
        var model = new WorksPlanningViewModel
        {
            ExpectedLeadContractorAppointmentMonth = pastDate.Month,
            ExpectedLeadContractorAppointmentYear = pastDate.Year,
            PreviousExpectedLeadContractorAppointmentDate = DateTime.Now.AddMonths(-12)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedLeadContractorAppointmentYear)
            .WithErrorMessage("Expected Lead Contractor Appointment must be in the future");
    }

    [Fact]
    public void Should_Have_Error_When_PreviousExpectedLeadContractorAppointmentDate_Exists_But_Current_Is_Null()
    {
        var model = new WorksPlanningViewModel
        {
            PreviousExpectedLeadContractorAppointmentDate = DateTime.Now.AddDays(30),
            ExpectedLeadContractorAppointmentMonth = null,
            ExpectedLeadContractorAppointmentYear = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedLeadContractorAppointmentYear)
            .WithErrorMessage("Expected Lead Contractor Appointment was supplied previously so must be kept");
    }

    #endregion

    #region Actual Lead Contractor Appointment Date Tests

    [Fact]
    public void Should_Have_Error_When_ActualLeadContractorAppointmentMonth_Is_Null_But_Year_Is_Provided()
    {
        var model = new WorksPlanningViewModel
        {
            ActualLeadContractorAppointmentMonth = null,
            ActualLeadContractorAppointmentYear = 2024
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ActualLeadContractorAppointmentMonth)
            .WithErrorMessage("Actual Lead Contractor Appointment month - required");
    }

    [Fact]
    public void Should_Have_Error_When_ActualLeadContractorAppointmentYear_Is_Null_But_Month_Is_Provided()
    {
        var model = new WorksPlanningViewModel
        {
            ActualLeadContractorAppointmentMonth = 12,
            ActualLeadContractorAppointmentYear = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ActualLeadContractorAppointmentYear)
            .WithErrorMessage("Actual Lead Contractor Appointment year - required");
    }

    [Fact]
    public void Should_Have_Error_When_ActualLeadContractorAppointmentMonth_Is_Out_Of_Range()
    {
        var model = new WorksPlanningViewModel
        {
            ActualLeadContractorAppointmentMonth = 0,
            ActualLeadContractorAppointmentYear = 2024
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ActualLeadContractorAppointmentMonth)
            .WithErrorMessage("Actual Lead Contractor Appointment month - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ActualLeadContractorAppointmentYear_Is_Out_Of_Range()
    {
        var model = new WorksPlanningViewModel
        {
            ActualLeadContractorAppointmentMonth = 12,
            ActualLeadContractorAppointmentYear = 3001
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ActualLeadContractorAppointmentYear)
            .WithErrorMessage("Actual Lead Contractor Appointment year - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ActualLeadContractorAppointmentDate_Is_In_Future()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new WorksPlanningViewModel
        {
            ActualLeadContractorAppointmentMonth = futureDate.Month,
            ActualLeadContractorAppointmentYear = futureDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ActualLeadContractorAppointmentYear)
            .WithErrorMessage("Actual Lead Contractor Appointment cannot be in the future");
    }

    [Fact]
    public void Should_Have_Error_When_ActualLeadContractorAppointmentDate_Is_Before_ExpectedDate()
    {
        var expectedDate = DateTime.Now.AddMonths(-2);
        var actualDate = DateTime.Now.AddMonths(-3);
        var model = new WorksPlanningViewModel
        {
            ExpectedLeadContractorAppointmentMonth = expectedDate.Month,
            ExpectedLeadContractorAppointmentYear = expectedDate.Year,
            ActualLeadContractorAppointmentMonth = actualDate.Month,
            ActualLeadContractorAppointmentYear = actualDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ActualLeadContractorAppointmentYear)
            .WithErrorMessage("Actual Lead Contractor Appointment cannot be before Expected Lead Contractor Appointment");
    }

    [Fact]
    public void Should_Have_Error_When_PreviousActualLeadContractorAppointmentDate_Exists_But_Current_Is_Null()
    {
        var model = new WorksPlanningViewModel
        {
            PreviousActualLeadContractorAppointmentDate = DateTime.Now.AddDays(-30),
            ActualLeadContractorAppointmentMonth = null,
            ActualLeadContractorAppointmentYear = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ActualLeadContractorAppointmentYear)
            .WithErrorMessage("Actual Lead Contractor Appointment was supplied previously so must be kept");
    }

    #endregion

    #region Expected Works Package Submission Date Tests

    [Fact]
    public void Should_Have_Error_When_ExpectedWorksPackageSubmissionMonth_Is_Null()
    {
        var model = new WorksPlanningViewModel
        {
            ExpectedWorksPackageSubmissionMonth = null,
            ExpectedWorksPackageSubmissionYear = 2025
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedWorksPackageSubmissionMonth)
            .WithErrorMessage("Expected Works Package Submission month - required");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedWorksPackageSubmissionYear_Is_Null()
    {
        var model = new WorksPlanningViewModel
        {
            ExpectedWorksPackageSubmissionMonth = 12,
            ExpectedWorksPackageSubmissionYear = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedWorksPackageSubmissionYear)
            .WithErrorMessage("Expected Works Package Submission year - required");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedWorksPackageSubmissionMonth_Is_Out_Of_Range()
    {
        var model = new WorksPlanningViewModel
        {
            ExpectedWorksPackageSubmissionMonth = 13,
            ExpectedWorksPackageSubmissionYear = 2025
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedWorksPackageSubmissionMonth)
            .WithErrorMessage("Expected Works Package Submission month - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedWorksPackageSubmissionYear_Is_Out_Of_Range()
    {
        var model = new WorksPlanningViewModel
        {
            ExpectedWorksPackageSubmissionMonth = 12,
            ExpectedWorksPackageSubmissionYear = 1999
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedWorksPackageSubmissionYear)
            .WithErrorMessage("Expected Works Package Submission year - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedWorksPackageSubmissionDate_Is_In_Past()
    {
        var pastDate = DateTime.Now.AddMonths(-6);
        var model = new WorksPlanningViewModel
        {
            ExpectedWorksPackageSubmissionMonth = pastDate.Month,
            ExpectedWorksPackageSubmissionYear = pastDate.Year,
            PreviousExpectedWorksPackageSubmissionDate = DateTime.Now.AddMonths(-12)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedWorksPackageSubmissionYear)
            .WithErrorMessage("Expected Works Package Submission must be in the future");
    }

    #endregion

    #region Valid Scenarios Tests

    [Fact]
    public void Should_Not_Have_Error_When_ExpectedTenderDate_Not_Required_Because_ActualDate_Exists()
    {
        var pastDate = DateTime.Now.AddMonths(-3);
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new WorksPlanningViewModel
        {
            ActualTenderMonth = pastDate.Month,
            ActualTenderYear = pastDate.Year,
            ExpectedLeadContractorAppointmentMonth = futureDate.Month,
            ExpectedLeadContractorAppointmentYear = futureDate.Year,
            ExpectedWorksPackageSubmissionMonth = futureDate.Month,
            ExpectedWorksPackageSubmissionYear = futureDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.ExpectedTenderMonth);
        result.ShouldNotHaveValidationErrorFor(x => x.ExpectedTenderYear);
    }

    [Fact]
    public void Should_Not_Have_Error_When_ExpectedLeadContractorAppointmentDate_Not_Required_Because_ActualDate_Exists()
    {
        var pastDate = DateTime.Now.AddMonths(-3);
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = futureDate.Month,
            ExpectedTenderYear = futureDate.Year,
            ActualLeadContractorAppointmentMonth = pastDate.Month,
            ActualLeadContractorAppointmentYear = pastDate.Year,
            ExpectedWorksPackageSubmissionMonth = futureDate.Month,
            ExpectedWorksPackageSubmissionYear = futureDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.ExpectedLeadContractorAppointmentMonth);
        result.ShouldNotHaveValidationErrorFor(x => x.ExpectedLeadContractorAppointmentYear);
    }

    [Fact]
    public void Should_Not_Have_Error_When_All_Required_Fields_Are_Valid()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = futureDate.Month,
            ExpectedTenderYear = futureDate.Year,
            ExpectedLeadContractorAppointmentMonth = futureDate.Month,
            ExpectedLeadContractorAppointmentYear = futureDate.Year,
            ExpectedWorksPackageSubmissionMonth = futureDate.Month,
            ExpectedWorksPackageSubmissionYear = futureDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_ActualTenderDate_Is_Valid_Past_Date()
    {
        var pastDate = DateTime.Now.AddMonths(-3);
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new WorksPlanningViewModel
        {
            ActualTenderMonth = pastDate.Month,
            ActualTenderYear = pastDate.Year,
            ExpectedLeadContractorAppointmentMonth = futureDate.Month,
            ExpectedLeadContractorAppointmentYear = futureDate.Year,
            ExpectedWorksPackageSubmissionMonth = futureDate.Month,
            ExpectedWorksPackageSubmissionYear = futureDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.ActualTenderMonth);
        result.ShouldNotHaveValidationErrorFor(x => x.ActualTenderYear);
    }

    [Fact]
    public void Should_Not_Have_Error_When_ActualLeadContractorAppointmentDate_Is_Valid_Past_Date()
    {
        var pastDate = DateTime.Now.AddMonths(-3);
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = futureDate.Month,
            ExpectedTenderYear = futureDate.Year,
            ActualLeadContractorAppointmentMonth = pastDate.Month,
            ActualLeadContractorAppointmentYear = pastDate.Year,
            ExpectedWorksPackageSubmissionMonth = futureDate.Month,
            ExpectedWorksPackageSubmissionYear = futureDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.ActualLeadContractorAppointmentMonth);
        result.ShouldNotHaveValidationErrorFor(x => x.ActualLeadContractorAppointmentYear);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Dates_Are_Same_Month_And_Year()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = futureDate.Month,
            ExpectedTenderYear = futureDate.Year,
            ExpectedLeadContractorAppointmentMonth = futureDate.Month,
            ExpectedLeadContractorAppointmentYear = futureDate.Year,
            ExpectedWorksPackageSubmissionMonth = futureDate.Month,
            ExpectedWorksPackageSubmissionYear = futureDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    #endregion

    #region Edge Cases Tests

    [Fact]
    public void Should_Not_Have_Error_When_ExpectedTenderDate_Same_As_Previous_Expected_Date()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = futureDate.Month,
            ExpectedTenderYear = futureDate.Year,
            PreviousExpectedTenderDate = new DateTime(futureDate.Year, futureDate.Month, 1),
            ExpectedLeadContractorAppointmentMonth = futureDate.Month,
            ExpectedLeadContractorAppointmentYear = futureDate.Year,
            ExpectedWorksPackageSubmissionMonth = futureDate.Month,
            ExpectedWorksPackageSubmissionYear = futureDate.Year
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.ExpectedTenderYear);
    }

    [Fact]
    public void Should_Have_Error_When_Both_Month_And_Year_Are_Zero()
    {
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = 0,
            ExpectedTenderYear = 0
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedTenderMonth);
        result.ShouldHaveValidationErrorFor(x => x.ExpectedTenderYear);
    }

    [Fact]
    public void Should_Have_Error_When_Month_Is_Negative()
    {
        var model = new WorksPlanningViewModel
        {
            ExpectedTenderMonth = -1,
            ExpectedTenderYear = 2025
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ExpectedTenderMonth);
    }

    #endregion
}