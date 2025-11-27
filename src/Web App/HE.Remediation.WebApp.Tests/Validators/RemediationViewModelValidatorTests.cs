using System;
using FluentValidation.TestHelper;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
using HE.Remediation.WebApp.TagHelpers;
using Xunit;

namespace HE.Remediation.WebApp.Tests.Validators;

public class RemediationViewModelValidatorTests
{
    private readonly RemediationViewModelValidator _validator = new();

    #region Full Completion of Works (Remediation Start) Tests

    [Fact]
    public void Should_Have_Error_When_FullCompletionOfWorks_Month_Is_Invalid()
    {
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "13",
                Year = "2025"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.FullCompletionOfWorksMonthYearInput.Month)
            .WithErrorMessage("Remediation Start month - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_FullCompletionOfWorks_Year_Is_Invalid()
    {
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "1999"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.FullCompletionOfWorksMonthYearInput.Year)
            .WithErrorMessage("Remediation Start year - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_FullCompletionOfWorks_Has_Invalid_Month_Year_Combination()
    {
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = null
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.FullCompletionOfWorksMonthYearInput)
            .WithErrorMessage("Remediation Start - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_FullCompletionOfWorks_Is_Required_But_Not_Provided()
    {
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = null,
                Year = null
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.FullCompletionOfWorksMonthYearInput)
            .WithErrorMessage("Remediation Start is required");
    }

    [Fact]
    public void Should_Have_Error_When_FullCompletionOfWorks_Is_In_Past()
    {
        var pastDate = DateTime.Now.AddMonths(-6);
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = pastDate.Month.ToString(),
                Year = pastDate.Year.ToString()
            },
            PreviousFullCompletionOfWorksDate = DateTime.Now.AddMonths(-12),
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = DateTime.Now.AddMonths(6).Month.ToString(),
                Year = DateTime.Now.AddMonths(6).Year.ToString()
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.FullCompletionOfWorksMonthYearInput)
            .WithErrorMessage("Remediation Start - Date must be in the future");
    }

    [Fact]
    public void Should_Have_Error_When_PreviousFullCompletionOfWorks_Exists_But_Current_Is_Null()
    {
        var model = new RemediationViewModel
        {
            PreviousFullCompletionOfWorksDate = DateTime.Now.AddMonths(6),
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = null,
                Year = null
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.FullCompletionOfWorksMonthYearInput)
            .WithErrorMessage("Remediation Start was supplied previously so must be kept");
    }

    [Fact]
    public void Should_Not_Have_Error_When_FullCompletionOfWorks_Is_Valid_Future_Date()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = futureDate.Month.ToString(),
                Year = futureDate.Year.ToString()
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = futureDate.AddMonths(3).Month.ToString(),
                Year = futureDate.AddMonths(3).Year.ToString()
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.FullCompletionOfWorksMonthYearInput.Month);
        result.ShouldNotHaveValidationErrorFor(x => x.FullCompletionOfWorksMonthYearInput.Year);
    }

    [Fact]
    public void Should_Not_Have_Error_When_FullCompletionOfWorks_Same_As_Previous_Date()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = futureDate.Month.ToString(),
                Year = futureDate.Year.ToString()
            },
            PreviousFullCompletionOfWorksDate = new DateTime(futureDate.Year, futureDate.Month, 1),
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = futureDate.AddMonths(3).Month.ToString(),
                Year = futureDate.AddMonths(3).Year.ToString()
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.FullCompletionOfWorksMonthYearInput);
    }

    #endregion

    #region Practical Completion Tests

    [Fact]
    public void Should_Have_Error_When_PracticalCompletion_Month_Is_Invalid()
    {
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "2025"
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "0",
                Year = "2025"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PracticalCompletionMonthYearInput.Month)
            .WithErrorMessage("Practical Completion month - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_PracticalCompletion_Year_Is_Invalid()
    {
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "2025"
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "3001"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PracticalCompletionMonthYearInput.Year)
            .WithErrorMessage("Practical Completion year - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_PracticalCompletion_Has_Invalid_Month_Year_Combination()
    {
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "2025"
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = null,
                Year = "2025"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PracticalCompletionMonthYearInput)
            .WithErrorMessage("Practical Completion - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_PracticalCompletion_Is_Required_But_Not_Provided()
    {
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "2025"
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = null,
                Year = null
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PracticalCompletionMonthYearInput)
            .WithErrorMessage("Practical Completion is required");
    }

    [Fact]
    public void Should_Have_Error_When_PracticalCompletion_Is_In_Past()
    {
        var futureRemediationDate = DateTime.Now.AddMonths(6);
        var pastPracticalDate = DateTime.Now.AddMonths(-3);
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = futureRemediationDate.Month.ToString(),
                Year = futureRemediationDate.Year.ToString()
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = pastPracticalDate.Month.ToString(),
                Year = pastPracticalDate.Year.ToString()
            },
            PreviousPracticalCompletionDate = DateTime.Now.AddMonths(-12)
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PracticalCompletionMonthYearInput)
            .WithErrorMessage("Practical Completion - Date must be in the future");
    }

    [Fact]
    public void Should_Have_Error_When_PracticalCompletion_Is_Before_RemediationStart()
    {
        var remediationDate = DateTime.Now.AddMonths(6);
        var practicalDate = DateTime.Now.AddMonths(3);
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = remediationDate.Month.ToString(),
                Year = remediationDate.Year.ToString()
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = practicalDate.Month.ToString(),
                Year = practicalDate.Year.ToString()
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PracticalCompletionMonthYearInput)
            .WithErrorMessage("Practical Completion cannot be before Remediation Start");
    }

    [Fact]
    public void Should_Have_Error_When_PreviousPracticalCompletion_Exists_But_Current_Is_Null()
    {
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "2025"
            },
            PreviousPracticalCompletionDate = DateTime.Now.AddMonths(9),
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = null,
                Year = null
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PracticalCompletionMonthYearInput)
            .WithErrorMessage("Practical Completion was supplied previously so must be kept");
    }

    [Fact]
    public void Should_Not_Have_Error_When_PracticalCompletion_Is_Valid_Future_Date()
    {
        var remediationDate = DateTime.Now.AddMonths(6);
        var practicalDate = DateTime.Now.AddMonths(12);
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = remediationDate.Month.ToString(),
                Year = remediationDate.Year.ToString()
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = practicalDate.Month.ToString(),
                Year = practicalDate.Year.ToString()
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PracticalCompletionMonthYearInput.Month);
        result.ShouldNotHaveValidationErrorFor(x => x.PracticalCompletionMonthYearInput.Year);
    }

    [Fact]
    public void Should_Not_Have_Error_When_PracticalCompletion_Is_Same_As_RemediationStart()
    {
        var sameDate = DateTime.Now.AddMonths(6);
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = sameDate.Month.ToString(),
                Year = sameDate.Year.ToString()
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = sameDate.Month.ToString(),
                Year = sameDate.Year.ToString()
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PracticalCompletionMonthYearInput);
    }

    [Fact]
    public void Should_Not_Have_Error_When_PracticalCompletion_Same_As_Previous_Date()
    {
        var futureDate = DateTime.Now.AddMonths(12);
        var remediationDate = DateTime.Now.AddMonths(6);
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = remediationDate.Month.ToString(),
                Year = remediationDate.Year.ToString()
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = futureDate.Month.ToString(),
                Year = futureDate.Year.ToString()
            },
            PreviousPracticalCompletionDate = new DateTime(futureDate.Year, futureDate.Month, 1)
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PracticalCompletionMonthYearInput);
    }

    #endregion

    #region Valid Scenarios Tests

    [Fact]
    public void Should_Not_Have_Error_When_All_Required_Fields_Are_Valid()
    {
        var remediationDate = DateTime.Now.AddMonths(6);
        var practicalDate = DateTime.Now.AddMonths(12);
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = remediationDate.Month.ToString(),
                Year = remediationDate.Year.ToString()
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = practicalDate.Month.ToString(),
                Year = practicalDate.Year.ToString()
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_Both_Dates_Are_In_Valid_Future()
    {
        var remediationDate = DateTime.Now.AddMonths(3);
        var practicalDate = DateTime.Now.AddMonths(9);
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = remediationDate.Month.ToString(),
                Year = remediationDate.Year.ToString()
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = practicalDate.Month.ToString(),
                Year = practicalDate.Year.ToString()
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    #endregion

    #region Edge Cases Tests

    [Fact]
    public void Should_Have_Error_When_FullCompletionOfWorks_Month_Is_Non_Numeric()
    {
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "abc",
                Year = "2025"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.FullCompletionOfWorksMonthYearInput.Month)
            .WithErrorMessage("Remediation Start month - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_PracticalCompletion_Year_Is_Non_Numeric()
    {
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "2025"
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "xyz"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PracticalCompletionMonthYearInput.Year)
            .WithErrorMessage("Practical Completion year - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_Month_Is_Negative()
    {
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "-1",
                Year = "2025"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.FullCompletionOfWorksMonthYearInput.Month)
            .WithErrorMessage("Remediation Start month - invalid");
    }

    [Fact]
    public void Should_Have_Error_When_Year_Is_Too_High()
    {
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "2025"
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "3001"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.PracticalCompletionMonthYearInput.Year)
            .WithErrorMessage("Practical Completion year - invalid");
    }

    #endregion

    #region Boundary Tests

    [Fact]
    public void Should_Not_Have_Error_When_Month_Is_1()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "1",
                Year = futureDate.Year.ToString()
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = futureDate.Year.ToString()
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.FullCompletionOfWorksMonthYearInput.Month);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Month_Is_12()
    {
        var futureDate = DateTime.Now.AddMonths(6);
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = futureDate.Year.ToString()
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = futureDate.AddMonths(3).Year.ToString()
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.FullCompletionOfWorksMonthYearInput.Month);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Year_Is_2000()
    {
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "2000"
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "2001"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.FullCompletionOfWorksMonthYearInput.Year);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Year_Is_3000()
    {
        var model = new RemediationViewModel
        {
            FullCompletionOfWorksMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "3000"
            },
            PracticalCompletionMonthYearInput = new MonthYearInputTagHelper.MonthYearInput
            {
                Month = "12",
                Year = "3000"
            }
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.PracticalCompletionMonthYearInput.Year);
    }

    #endregion
}