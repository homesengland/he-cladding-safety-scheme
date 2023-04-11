using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.Profile.SetUserResponsibleEntityType;

public class SetUserResponsibleEntityTypeHandler : IRequestHandler<SetUserResponsibleEntityTypeRequest>
{
    private readonly IDbConnectionWrapper _db;
    private readonly IUserService _userService;
    private readonly IApplicationDataProvider _adp;

    public SetUserResponsibleEntityTypeHandler(IDbConnectionWrapper db, IUserService userService, IApplicationDataProvider adp)
    {
        _db = db;
        _userService = userService;
        _adp = adp;
    }

    public async Task<Unit> Handle(SetUserResponsibleEntityTypeRequest request, CancellationToken cancellationToken)
    {
        var userId = _adp.GetUserId();

        if (userId is null)
            throw new EntityNotFoundException(
                "Cannot scaffold the current user company details because the current user could be determined.");

        // Scaffold company entities for our current user if they're a company entity type
        // ...but only if they haven't been scaffolded previously.
        if (request.ResponsibleEntityType == EResponsibleEntityType.Company)
        {
            if (!await _userService.IsUserCompanyDetailsDataCreated(userId.Value))
            {
                await _db.ExecuteAsync("InsertCompanyDetailsForUserId", new
                {
                    UserId = userId,
                    CompanyName = (string)null,
                    CompanyRegistrationNumber = (string)null,
                    UserRoleInCompany = (string)null
                });
            }

            if (!await _userService.IsUserCompanyAddressDataCreated(userId.Value))
            {
                await _db.ExecuteAsync("InsertCompanyAddressForUserId", new
                {
                    userId,
                    NameNumber = string.Empty,
                    AddressLine1 = string.Empty,
                    AddressLine2 = (string)null,
                    City = string.Empty,
                    County = (string)null,
                    Postcode = string.Empty,
                    LocalAuthority = (string)null
                });
            }
        }

        // Set-up the profile completion stages
        await (request.ResponsibleEntityType switch
        {
            EResponsibleEntityType.Company => SetUpCompanyCompletionSteps(),
            EResponsibleEntityType.Individual => SetUpIndividualCompletionSteps(),
            _ => throw new ArgumentOutOfRangeException(
                $"Cannot set up completion steps because an invalid responsible entity type has been selected ({(int) request.ResponsibleEntityType}).")
        });

        await _db.ExecuteAsync("SetUserResponsibleEntityTypeByUserId", new
        {
            userId,
            ResponsibleEntityTypeId = (int) request.ResponsibleEntityType
        });

        await _userService.SetUserProfileStageCompletionStatus(
            EUserProfileStage.ResponsibleEntityTypeSelection, 
            userId.Value,
            true);

        _adp.SetUserProfileStageCompletionStatus(EUserProfileStage.ResponsibleEntityTypeSelection, 
                                                 request.ResponsibleEntityType);
        return Unit.Value;
    }

    private async Task SetUpCompanyCompletionSteps()
    {
        var userId = _adp.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot scaffold the current user company details because the current user could be determined.");
        }

        var existingSteps = await _userService.GetUserProfileCompletionData(userId.Value);

        // Only set the values to false if they haven't already been set before, so we don't override previously
        // completed steps with a false negative status.
        existingSteps.IsCompanyDetailsComplete ??= false;
        existingSteps.IsCompanyAddressComplete ??= false;
        existingSteps.IsSecondaryContactInformationComplete = false;

        await _userService.UpdateUserProfileCompletionStages(userId.Value, existingSteps);
    }

    private async Task SetUpIndividualCompletionSteps()
    {
        var userId = _adp.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot scaffold the current user details because the current user could be determined.");
        }

        var existingSteps = await _userService.GetUserProfileCompletionData(userId.Value);

        // Only set the values to false if they haven't already been set before, so we don't override previously
        // completed steps with a false negative status.
        existingSteps.IsSecondaryContactInformationComplete = false;

        await _userService.UpdateUserProfileCompletionStages(userId.Value, existingSteps);        
    }
}