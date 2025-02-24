﻿using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class WhenStartOnSiteViewModelValidator : AbstractValidator<WhenStartOnSiteViewModel>
{
    private const int MaxYear = 2040;

    public WhenStartOnSiteViewModelValidator()
    {
        RuleFor(x => x.StartMonth)
            .NotNull()
            .WithMessage("Please provide a month")
            .InclusiveBetween(1, 12)
            .WithMessage("Please provide a month");

        RuleFor(x => x.StartYear)
            .NotNull()
            .WithMessage("Please provide a year")
            .NotEmpty()
            .WithMessage("Please provide a year")
            .LessThanOrEqualTo(MaxYear)
            .WithMessage($"Please provide a year no later than {MaxYear}");

        RuleFor(x => new { x.StartMonth, x.StartYear })
            .Must(x => BeInTheFuture(x.StartMonth, x.StartYear))
            .WithName("StartMonth")
            .WithMessage("The date must be in the future");
    }

    private bool BeInTheFuture(int? month, int? year)
    {
        if (month is null or < 1 or > 12) return false;
        if (year is null or < 1 or > MaxYear) return false;

        var date = new DateTime(year.Value, month.Value, 1).AddMonths(1).AddDays(-1);

        return date.Date >= DateTime.Today;
    }
}
