using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.SetCompanyAddressForCurrentUser;

public class SetCompanyAddressForCurrentUserHandler : IRequestHandler<SetCompanyAddressForCurrentUserRequest>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IDbConnectionWrapper _db;
    private readonly IUserService _userService;

    public SetCompanyAddressForCurrentUserHandler(IApplicationDataProvider adp, IDbConnectionWrapper db, IUserService userService)
    {
        _adp = adp;
        _db = db;
        _userService = userService;
    }

    public async Task<Unit> Handle(SetCompanyAddressForCurrentUserRequest request, CancellationToken cancellationToken)
    {
        var userId = _adp.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot set current user company address because the current user could be determined.");
        }

        ParsedAddress parsedAddress = PostCodeUtility.ParseAddressJson(request.SelectedAddressId);
        if (parsedAddress != null)
        {            
            await _db.ExecuteAsync("UpdateCompanyAddressByUserId", new
            {
                userId,
                parsedAddress.NameNumber,
                parsedAddress.AddressLine1,
                parsedAddress.AddressLine2,
                parsedAddress.City,
                parsedAddress.LocalAuthority,
                parsedAddress.County,
                parsedAddress.Postcode,
                parsedAddress.SubBuildingName,
                parsedAddress.BuildingName,
                parsedAddress.BuildingNumber,
                parsedAddress.Street,
                parsedAddress.Town,
                parsedAddress.AdminArea,
                parsedAddress.UPRN,
                parsedAddress.AddressLines,
                parsedAddress.XCoordinate,
                parsedAddress.YCoordinate,
                parsedAddress.Toid,
                parsedAddress.BuildingType
            });                

            await _userService.SetUserProfileStageCompletionStatus(
            EUserProfileStage.CompanyAddress,
            userId.Value,
            true);

            _adp.SetUserProfileStageCompletionStatus(EUserProfileStage.CompanyAddress);
        }
        
        return Unit.Value;
    }
}