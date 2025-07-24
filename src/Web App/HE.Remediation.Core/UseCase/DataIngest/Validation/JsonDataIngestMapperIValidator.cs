using FluentValidation;

namespace HE.Remediation.Core.UseCase.DataIngest.Validation
{
    public class JsonDataIngestMapperIValidator : AbstractValidator<ImportedDataRow>
    {
        public JsonDataIngestMapperIValidator()
        {
            RuleFor(x => x.BuildingName)
                .NotEmpty().WithMessage("Blank Building_Name")
                .MaximumLength(200).WithMessage("Building_Name cannot exceed 200 characters");

            RuleFor(x => x.PostCode)
                .NotEmpty().WithMessage("Blank Postcode")
                .ValidGovPostcode();

            RuleFor(x => x.LocalAuthority)
                .NotEmpty().WithMessage("Blank Local_Authority");

            RuleFor(x => x.ResidentialUnitsCount)
                .NotEmpty().WithMessage("Blank Dwelling_Units_Responsible_For")
                .LessThanOrEqualTo(999).WithMessage("Dwelling_Units_Responsible_For can be no more than 999");

            RuleFor(x => x.NumberOfStoreys)
                .NotEmpty().WithMessage("Height_Bracket not valid");

            RuleFor(x => x.OrganisationName)
                .NotEmpty().WithMessage("Blank RP_Name");

            RuleFor(x => x.RegistrationNumber)
                .NotEmpty().WithMessage("Blank Provider_Number")
                .Length(4, 6).WithMessage("Provider_Number must be between 4 and 6 characters");

            RuleFor(x => x.HowManyLeaseholders)
               .InclusiveBetween(0, 999).WithMessage("Leasehold must be between 0 and 999");
        }
    }
}
