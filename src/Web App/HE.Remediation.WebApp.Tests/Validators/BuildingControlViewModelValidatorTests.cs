using System;
using FluentValidation.TestHelper;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
using HE.Remediation.WebApp.TagHelpers;
using Xunit;

namespace HE.Remediation.WebApp.Tests.Validators;

public class BuildingControlViewModelValidatorTests
{
    private readonly BuildingControlViewModelValidator _validator = new();

    #region Expected Application Date Tests

    [Fact]
    public void Should_Have_Error_When_ExpectedApplicationDate_Month_Is_Invalid()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlExpectedApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "13",
                Year = "2025"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput.Month)
            .WithErrorMessage("Expected Application month - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedApplicationDate_Year_Is_Invalid()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlExpectedApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "1999"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput.Year)
            .WithErrorMessage("Expected Application year - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedApplicationDate_Has_Invalid_Month_Year_Combination()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlExpectedApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = null
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput)
            .WithErrorMessage("Expected Application - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedApplicationDate_Required_But_Not_Provided()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlExpectedApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = null,
                Year = null
            },
            BuildingControlActualApplicationDate = null,
            BuildingControlDecisionDate = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput)
            .WithErrorMessage("Expected Application is required");
    }

    [Fact]
    public void Should_Have_Error_When_ExpectedApplicationDate_Is_In_Past()
    {
        var pastDate = DateTime.Now.AddMonths(-6);
        var model = new BuildingControlViewModel
        {
            BuildingControlExpectedApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = pastDate.Month.ToString(),
                Year = pastDate.Year.ToString()
            },
            PreviousBuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(-12)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput)
            .WithErrorMessage("Expected Application must be in the future");
    }

    [Fact]
    public void Should_Have_Error_When_PreviousExpectedApplicationDate_Exists_But_Current_Is_Null()
    {
        var model = new BuildingControlViewModel
        {
            PreviousBuildingControlExpectedApplicationDate = DateTime.Now.AddMonths(6),
            BuildingControlExpectedApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = null,
                Year = null
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput)
            .WithErrorMessage("Expected Application was supplied previously so must be kept");
    }

    #endregion

    #region Actual Application Date Tests

    [Fact]
    public void Should_Have_Error_When_ActualApplicationDate_Month_Is_Invalid()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlActualApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "0",
                Year = "2024"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlActualApplicationDateMonthYearInput.Month)
            .WithErrorMessage("Actual Application month - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ActualApplicationDate_Year_Is_Invalid()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlActualApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "3001"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlActualApplicationDateMonthYearInput.Year)
            .WithErrorMessage("Actual Application year - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ActualApplicationDate_Has_Invalid_Month_Year_Combination()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlActualApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = null,
                Year = "2024"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlActualApplicationDateMonthYearInput)
            .WithErrorMessage("Actual Application - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ActualApplicationDate_Is_In_Future()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new BuildingControlViewModel
        {
            BuildingControlActualApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = futureDate.Month.ToString(),
                Year = futureDate.Year.ToString()
            },
            BuildingControlActualApplicationDate = futureDate
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlActualApplicationDateMonthYearInput)
            .WithErrorMessage("Actual Application cannot be in the future");
    }

    [Fact]
    public void Should_Have_Error_When_ActualApplicationDate_Is_Before_ExpectedDate()
    {
        var expectedDate = DateTime.Now.AddMonths(-2);
        var actualDate = DateTime.Now.AddMonths(-6);
        var model = new BuildingControlViewModel
        {
            BuildingControlExpectedApplicationDate = expectedDate,
            BuildingControlActualApplicationDate = actualDate,
            BuildingControlActualApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = actualDate.Month.ToString(),
                Year = actualDate.Year.ToString()
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlActualApplicationDateMonthYearInput)
            .WithErrorMessage("Actual Application cannot be before Expected Application Date");
    }

    [Fact]
    public void Should_Have_Error_When_PreviousActualApplicationDate_Exists_But_Current_Is_Null()
    {
        var model = new BuildingControlViewModel
        {
            PreviousBuildingControlActualApplicationDate = DateTime.Now.AddMonths(-3),
            BuildingControlActualApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = null,
                Year = null
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlActualApplicationDateMonthYearInput)
            .WithErrorMessage("Actual Application was supplied previously so must be kept");
    }

    #endregion

    #region Validation Date Tests

    [Fact]
    public void Should_Have_Error_When_ValidationDate_Month_Is_Invalid()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlValidationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "13",
                Year = "2024"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlValidationDateMonthYearInput.Month)
            .WithErrorMessage("Validation Date month - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ValidationDate_Year_Is_Invalid()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlValidationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "1999"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlValidationDateMonthYearInput.Year)
            .WithErrorMessage("Validation Date year - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ValidationDate_Has_Invalid_Month_Year_Combination()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlValidationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = null
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlValidationDateMonthYearInput)
            .WithErrorMessage("Validation Date - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_ValidationDate_Is_In_Future()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new BuildingControlViewModel
        {
            BuildingControlValidationDate = futureDate,
            BuildingControlValidationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = futureDate.Month.ToString(),
                Year = futureDate.Year.ToString()
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlValidationDateMonthYearInput)
            .WithErrorMessage("Validation Date cannot be in the future");
    }

    [Fact]
    public void Should_Have_Error_When_ValidationDate_Is_Before_ActualApplicationDate()
    {
        var actualDate = DateTime.Now.AddMonths(-3);
        var validationDate = DateTime.Now.AddMonths(-6);
        var model = new BuildingControlViewModel
        {
            BuildingControlActualApplicationDate = actualDate,
            BuildingControlValidationDate = validationDate,
            BuildingControlValidationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = validationDate.Month.ToString(),
                Year = validationDate.Year.ToString()
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlValidationDateMonthYearInput)
            .WithErrorMessage("Validation Date must be on or after Actual Application date");
    }

    [Fact]
    public void Should_Have_Error_When_PreviousValidationDate_Exists_But_Current_Is_Null()
    {
        var model = new BuildingControlViewModel
        {
            PreviousBuildingControlValidationDate = DateTime.Now.AddMonths(-3),
            BuildingControlValidationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = null,
                Year = null
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlValidationDateMonthYearInput)
            .WithErrorMessage("Validation Date was supplied previously so must be kept");
    }

    #endregion

    #region Decision Date Tests

    [Fact]
    public void Should_Have_Error_When_DecisionDate_Month_Is_Invalid()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlDecisionDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "0",
                Year = "2024"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlDecisionDateMonthYearInput.Month)
            .WithErrorMessage("Decision Date month - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_DecisionDate_Year_Is_Invalid()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlDecisionDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "3001"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlDecisionDateMonthYearInput.Year)
            .WithErrorMessage("Decision Date year - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_DecisionDate_Has_Invalid_Month_Year_Combination()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlDecisionDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = null,
                Year = "2024"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlDecisionDateMonthYearInput)
            .WithErrorMessage("Decision Date - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_DecisionDate_Is_In_Future()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new BuildingControlViewModel
        {
            BuildingControlDecisionDate = futureDate,
            BuildingControlDecisionDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = futureDate.Month.ToString(),
                Year = futureDate.Year.ToString()
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlDecisionDateMonthYearInput)
            .WithErrorMessage("Decision Date cannot be in the future");
    }

    [Fact]
    public void Should_Have_Error_When_PreviousDecisionDate_Exists_But_Current_Is_Null()
    {
        var model = new BuildingControlViewModel
        {
            PreviousBuildingControlDecisionDate = DateTime.Now.AddMonths(-3),
            BuildingControlDecisionDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = null,
                Year = null
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlDecisionDateMonthYearInput)
            .WithErrorMessage("Decision Date was supplied previously so must be kept");
    }

    #endregion

    #region Gateway2Reference Tests

    [Fact]
    public void Should_Have_Error_When_Gateway2Reference_Is_Not_12_Characters()
    {
        var model = new BuildingControlViewModel
        {
            Gateway2Reference = "ABC123"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Gateway2Reference)
            .WithErrorMessage("Gateway 2 application reference must be 12 characters");
    }

    [Fact]
    public void Should_Have_Error_When_Gateway2Reference_Is_Too_Long()
    {
        var model = new BuildingControlViewModel
        {
            Gateway2Reference = "ABC1234567890"
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Gateway2Reference)
            .WithErrorMessage("Gateway 2 application reference must be 12 characters");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Gateway2Reference_Is_Null()
    {
        var model = new BuildingControlViewModel
        {
            Gateway2Reference = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Gateway2Reference);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Gateway2Reference_Is_Empty()
    {
        var model = new BuildingControlViewModel
        {
            Gateway2Reference = ""
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Gateway2Reference);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Gateway2Reference_Is_Exactly_12_Characters()
    {
        var model = new BuildingControlViewModel
        {
            Gateway2Reference = "ABC123456789"
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Gateway2Reference);
    }

    #endregion

    #region BuildingControlDecisionType Tests

    [Fact]
    public void Should_Have_Error_When_BuildingControlDecisionType_Is_Null_And_DecisionDate_Exists()
    {
        var pastDate = DateTime.Now.AddMonths(-3);
        var model = new BuildingControlViewModel
        {
            BuildingControlDecisionDate = pastDate,
            BuildingControlDecisionType = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlDecisionType)
            .WithErrorMessage("Provide a Building Control Decision");
    }

    [Fact]
    public void Should_Not_Have_Error_When_BuildingControlDecisionType_Is_Provided_With_DecisionDate()
    {
        var pastDate = DateTime.Now.AddMonths(-3);
        var model = new BuildingControlViewModel
        {
            BuildingControlDecisionDate = pastDate,
            BuildingControlDecisionType = EBuildingControlDecisionType.Approved
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.BuildingControlDecisionType);
    }

    [Fact]
    public void Should_Not_Have_Error_When_BuildingControlDecisionType_Is_Null_And_No_DecisionDate()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlDecisionDate = null,
            BuildingControlDecisionType = null
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.BuildingControlDecisionType);
    }

    #endregion

    #region Valid Scenarios Tests

    [Fact]
    public void Should_Not_Have_Error_When_Only_ExpectedApplicationDate_Is_Provided()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new BuildingControlViewModel
        {
            BuildingControlExpectedApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = futureDate.Month.ToString(),
                Year = futureDate.Year.ToString()
            },
            BuildingControlActualApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput(),
            BuildingControlValidationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput(),
            BuildingControlDecisionDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput()
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput);
    }

    [Fact]
    public void Should_Not_Have_Error_When_ActualApplicationDate_Is_Valid()
    {
        var pastDate = DateTime.Now.AddMonths(-3);
        var model = new BuildingControlViewModel
        {
            BuildingControlActualApplicationDate = pastDate,
            BuildingControlActualApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = pastDate.Month.ToString(),
                Year = pastDate.Year.ToString()
            },
            BuildingControlExpectedApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput(),
            BuildingControlValidationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput(),
            BuildingControlDecisionDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput()
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.BuildingControlActualApplicationDateMonthYearInput);
    }

    [Fact]
    public void Should_Not_Have_Error_When_ValidationDate_Is_Same_As_ActualApplicationDate()
    {
        var pastDate = DateTime.Now.AddMonths(-3);
        var model = new BuildingControlViewModel
        {
            BuildingControlActualApplicationDate = pastDate,
            BuildingControlValidationDate = pastDate,
            BuildingControlValidationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = pastDate.Month.ToString(),
                Year = pastDate.Year.ToString()
            },
            BuildingControlExpectedApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput(),
            BuildingControlActualApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput(),
            BuildingControlDecisionDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput()
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.BuildingControlValidationDateMonthYearInput);
    }

    [Fact]
    public void Should_Not_Have_Error_When_All_Dates_Are_In_Valid_Sequence()
    {
        var previousExpectedDate = new DateTime(2025, 1, 1);
        var expectedDate = new DateTime(2025, 1, 1);
        var actualDate = DateTime.Now.AddMonths(-1);
        var validationDate = DateTime.Now.AddMonths(-1);
        var decisionDate = DateTime.Now.AddMonths(-1);

        var model = new BuildingControlViewModel
        {
            PreviousBuildingControlExpectedApplicationDate = previousExpectedDate,
            BuildingControlExpectedApplicationDate = expectedDate,
            BuildingControlActualApplicationDate = actualDate,
            BuildingControlValidationDate = validationDate,
            BuildingControlDecisionDate = decisionDate,
            BuildingControlDecisionType = EBuildingControlDecisionType.Approved,
            BuildingControlExpectedApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = expectedDate.Month.ToString(),
                Year = expectedDate.Year.ToString()
            },
            BuildingControlActualApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = actualDate.Month.ToString(),
                Year = actualDate.Year.ToString()
            },
            BuildingControlValidationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = validationDate.Month.ToString(),
                Year = validationDate.Year.ToString()
            },
            BuildingControlDecisionDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = decisionDate.Month.ToString(),
                Year = decisionDate.Year.ToString()
            },
            Gateway2Reference = "ABC123456789"
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    #endregion

    #region Edge Cases Tests

    [Fact]
    public void Should_Have_Error_When_Month_Is_Non_Numeric_String()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlExpectedApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "abc",
                Year = "2025"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput.Month)
            .WithErrorMessage("Expected Application month - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_Year_Is_Non_Numeric_String()
    {
        var model = new BuildingControlViewModel
        {
            BuildingControlExpectedApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "abc"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput.Year)
            .WithErrorMessage("Expected Application year - invalid");
    }

    [Fact]
    public void Should_Not_Have_Error_When_ExpectedApplicationDate_Not_Required_Because_ActualDate_Exists()
    {
        var pastDate = DateTime.Now.AddMonths(-3);
        var model = new BuildingControlViewModel
        {
            BuildingControlExpectedApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = null,
                Year = null
            },
            BuildingControlActualApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput() { Month = pastDate.Month.ToString(), Year = pastDate.Year.ToString() }
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput);
    }

    [Fact]
    public void Should_Not_Have_Error_When_ExpectedApplicationDate_Not_Required_Because_DecisionDate_Exists()
    {
        var pastDate = DateTime.Now.AddMonths(-3);
        var model = new BuildingControlViewModel
        {
            BuildingControlExpectedApplicationDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = null,
                Year = null
            },
            BuildingControlDecisionDateMonthYearInput = new MonthYearInputTagHelper.MonthYearInput() { Month = pastDate.Month.ToString(), Year = pastDate.Year.ToString() }
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.BuildingControlExpectedApplicationDateMonthYearInput);
    }

    #endregion
}