using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class LeaseholderResidentEvidenceViewModelValidator : AbstractValidator<LeaseholderResidentEvidenceViewModel>
{
    private const int Mb = 50;
    private const int FileSizeLimit = Mb * 1024 * 1024; // 50 MB

    public LeaseholderResidentEvidenceViewModelValidator()
    {

        When(x => x.SubmitAction == ESubmitAction.Upload, () =>
        {
            RuleFor(x => x.File)
                .Must(x => x == null || x.Length <= FileSizeLimit)
                .WithMessage($"File is larger than {Mb}mb");
        });


        RuleFor(x => x.LastCommunicationDateMonth)
            .NotNull().WithMessage("Please enter the last communication month")
            .NotEmpty().WithMessage("Please enter the last communication month")
            .InclusiveBetween(1, 12).WithMessage("Please enter a valid month (1–12)");

        RuleFor(x => x.LastCommunicationDateYear)
            .NotNull().WithMessage("Please enter the last communication year")
            .NotEmpty().WithMessage("Please enter the last communication year");


        RuleFor(x => x)
            .Must(x => !IsFutureMonthYear(x.LastCommunicationDateMonth, x.LastCommunicationDateYear))
            .WithName(nameof(LeaseholderResidentEvidenceViewModel.LastCommunicationDateMonth))
            .WithMessage("Date cannot be in the future")
            .When(x => x.LastCommunicationDateMonth.HasValue && x.LastCommunicationDateYear.HasValue);
    }

    private static bool IsFutureMonthYear(int? month, int? year)
    {
        if (month is null or < 1 or > 12) return false;
        if (year is null) return false;

        var input = new DateTime(year.Value, month.Value, 1);
        var current = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        return input > current;
    }
}