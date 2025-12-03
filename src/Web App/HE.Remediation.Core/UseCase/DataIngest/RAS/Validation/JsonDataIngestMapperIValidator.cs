using FluentValidation;

namespace HE.Remediation.Core.UseCase.DataIngest.RAS.Validation;

public class JsonDataIngestMapperIValidator : AbstractValidator<ImportedDataRow>
{
    public JsonDataIngestMapperIValidator()
    {
        RuleFor(x => x.Developer)
            .NotEmpty().WithMessage("Developer cannot be blank");
    }
}
