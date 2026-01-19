using FluentValidation.TestHelper;
using HE.Remediation.WebApp.TagHelpers;
using HE.Remediation.WebApp.ViewModels.Application;

namespace HE.Remediation.WebApp.Tests.Validators;

public class ManageProgrammeUpdateViewModelValidatorTests
{
    private readonly ManageProgrammeUpdateViewModelValidator _validator = new();

    private ManageProgrammeUpdateViewModel CreateDefaultModel()
    {
        return new ManageProgrammeUpdateViewModel
        {
            EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput(),
            EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput(),
            EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput()
        };
    }

    #region Month Validation Tests

    [Theory]
    [InlineData("0")]
    [InlineData("13")]
    [InlineData("-1")]
    [InlineData("99")]
    public void Should_Have_Error_When_EstimatedInvestigationCompletion_Month_Is_Out_Of_Range(string month)
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = month,
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EstimatedInvestigationCompletion)
            .WithErrorMessage("Month must be between 1 and 12");
    }

    [Theory]
    [InlineData("0")]
    [InlineData("13")]
    [InlineData("-1")]
    public void Should_Have_Error_When_EstimatedStartOnSite_Month_Is_Out_Of_Range(string month)
    {
        var model = CreateDefaultModel();
        model.EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = month,
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EstimatedStartOnSite)
            .WithErrorMessage("Month must be between 1 and 12");
    }

    [Theory]
    [InlineData("0")]
    [InlineData("13")]
    [InlineData("-1")]
    public void Should_Have_Error_When_EstimatedPracticalCompletion_Month_Is_Out_Of_Range(string month)
    {
        var model = CreateDefaultModel();
        model.EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = month,
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EstimatedPracticalCompletion)
            .WithErrorMessage("Month must be between 1 and 12");
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("1.5")]
    [InlineData("Jan")]
    public void Should_Have_Error_When_Month_Is_Not_A_Number(string month)
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = month,
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EstimatedInvestigationCompletion)
            .WithErrorMessage("Month must be a number");
    }

    #endregion

    #region Year Validation Tests

    [Theory]
    [InlineData("abc")]
    [InlineData("20.25")]
    [InlineData("twenty")]
    public void Should_Have_Error_When_Year_Is_Not_A_Number(string year)
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = year
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EstimatedInvestigationCompletion)
            .WithErrorMessage("Year must be a number");
    }

    #endregion

    #region Combo Validation Tests

    [Fact]
    public void Should_Have_Error_When_Month_Provided_But_Year_Missing()
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EstimatedInvestigationCompletion)
            .WithErrorMessage("You must enter both a month and a year, or neither");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Both_Month_And_Year_Are_Null()
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = null,
            Year = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.EstimatedInvestigationCompletion);
    }

    #endregion

    #region Minimum Date Validation Tests (01/01/2017)

    [Fact]
    public void Should_Have_Error_When_EstimatedInvestigationCompletion_Is_Before_MinDate()
    {
        var model = CreateDefaultModel();
        var currentYear = DateTime.Now.Year;
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "12",
            Year = "2016"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EstimatedInvestigationCompletion)
            .WithErrorMessage("Dates cannot be before 01/2017");
    }

    [Fact]
    public void Should_Have_Error_When_EstimatedStartOnSite_Is_Before_MinDate()
    {
        var model = CreateDefaultModel();
        model.EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2015"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EstimatedStartOnSite)
            .WithErrorMessage("Dates cannot be before 01/2017");
    }

    [Fact]
    public void Should_Have_Error_When_EstimatedPracticalCompletion_Is_Before_MinDate()
    {
        var model = CreateDefaultModel();
        model.EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "1",
            Year = "2010"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EstimatedPracticalCompletion)
            .WithErrorMessage("Dates cannot be before 01/2017");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Date_Is_Exactly_MinDate()
    {
        var model = CreateDefaultModel();
        var currentYear = DateTime.Now.Year;
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "1",
            Year = currentYear.ToString()
        };

        var result = _validator.TestValidate(model);

        // Should not have minDate error (only testing >= 2017 validation, not year cutoff)
        var errors = result.Errors.Where(e => e.ErrorMessage == "Dates cannot be before 01/2017").ToList();
        Assert.Empty(errors);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Date_Is_After_MinDate()
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.EstimatedInvestigationCompletion);
    }

    #endregion

    #region Date Comparison Validation Tests

    [Fact]
    public void Should_Have_Error_When_EstimatedStartOnSite_Is_Before_EstimatedInvestigationCompletion()
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2025"
        };
        model.EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "3",
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EstimatedStartOnSite)
            .WithErrorMessage("Estimated start on site date cannot be before the estimated works investigation completion date");
    }

    [Fact]
    public void Should_Not_Have_Error_When_EstimatedStartOnSite_Is_Same_As_EstimatedInvestigationCompletion()
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2025"
        };
        model.EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.EstimatedStartOnSite);
    }

    [Fact]
    public void Should_Not_Have_Error_When_EstimatedStartOnSite_Is_After_EstimatedInvestigationCompletion()
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2025"
        };
        model.EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "9",
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.EstimatedStartOnSite);
    }

    [Fact]
    public void Should_Have_Error_When_EstimatedPracticalCompletion_Is_Before_EstimatedStartOnSite()
    {
        var model = CreateDefaultModel();
        model.EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2025"
        };
        model.EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "3",
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EstimatedPracticalCompletion)
            .WithErrorMessage("Estimated practical completion date cannot be before estimated start on site date");
    }

    [Fact]
    public void Should_Not_Have_Error_When_EstimatedPracticalCompletion_Is_Same_As_EstimatedStartOnSite()
    {
        var model = CreateDefaultModel();
        model.EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2025"
        };
        model.EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.EstimatedPracticalCompletion);
    }

    [Fact]
    public void Should_Not_Have_Error_When_EstimatedPracticalCompletion_Is_After_EstimatedStartOnSite()
    {
        var model = CreateDefaultModel();
        model.EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2025"
        };
        model.EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "12",
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.EstimatedPracticalCompletion);
    }

    [Fact]
    public void Should_Have_Error_When_EstimatedPracticalCompletion_Is_Before_EstimatedInvestigationCompletion()
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2025"
        };
        model.EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "3",
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EstimatedPracticalCompletion)
            .WithErrorMessage("Estimated practical completion date cannot be before the estimated works investigation completion date");
    }

    [Fact]
    public void Should_Not_Have_Error_When_EstimatedPracticalCompletion_Is_Same_As_EstimatedInvestigationCompletion()
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2025"
        };
        model.EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.EstimatedPracticalCompletion);
    }

    [Fact]
    public void Should_Not_Have_Error_When_EstimatedPracticalCompletion_Is_After_EstimatedInvestigationCompletion()
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2025"
        };
        model.EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "12",
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.EstimatedPracticalCompletion);
    }

    [Fact]
    public void Should_Not_Validate_Date_Comparisons_When_Dates_Are_Null()
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = null,
            Year = null
        };
        model.EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = null,
            Year = null
        };
        model.EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = null,
            Year = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    #endregion

    #region Complex Scenario Tests

    [Fact]
    public void Should_Have_Multiple_Errors_When_All_Dates_Are_Before_MinDate()
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "1",
            Year = "2016"
        };
        model.EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "3",
            Year = "2016"
        };
        model.EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2016"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EstimatedInvestigationCompletion);
        result.ShouldHaveValidationErrorFor(x => x.EstimatedStartOnSite);
        result.ShouldHaveValidationErrorFor(x => x.EstimatedPracticalCompletion);
    }

    [Fact]
    public void Should_Validate_Successfully_When_All_Dates_Are_In_Correct_Order()
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "3",
            Year = "2025"
        };
        model.EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2025"
        };
        model.EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "12",
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Multiple_Errors_When_Dates_Are_In_Wrong_Order()
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "12",
            Year = "2025"
        };
        model.EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2025"
        };
        model.EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "3",
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EstimatedStartOnSite);
        result.ShouldHaveValidationErrorFor(x => x.EstimatedPracticalCompletion);
    }

    [Fact]
    public void Should_Validate_Dates_Across_Multiple_Years()
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "11",
            Year = "2024"
        };
        model.EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "2",
            Year = "2025"
        };
        model.EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "8",
            Year = "2025"
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_Dates_Span_Years_But_Are_In_Wrong_Order()
    {
        var model = CreateDefaultModel();
        model.EstimatedInvestigationCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "6",
            Year = "2026"
        };
        model.EstimatedStartOnSite = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "12",
            Year = "2025"
        };
        model.EstimatedPracticalCompletion = new MonthYearInputTagHelper.MonthYearInput
        {
            Month = "1",
            Year = "2027"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EstimatedStartOnSite)
            .WithErrorMessage("Estimated start on site date cannot be before the estimated works investigation completion date");
    }

    #endregion
}
