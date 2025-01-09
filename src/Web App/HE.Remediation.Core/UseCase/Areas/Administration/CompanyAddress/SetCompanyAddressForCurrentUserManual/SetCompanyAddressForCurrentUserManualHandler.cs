using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.SetCompanyAddressForCurrentUserManual;

public class SetCompanyAddressForCurrentUserManualHandler: IRequestHandler<SetCompanyAddressForCurrentUserManualRequest>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IDbConnectionWrapper _db;
    private readonly IUserService _userService;

    public SetCompanyAddressForCurrentUserManualHandler(IApplicationDataProvider adp, IDbConnectionWrapper db, IUserService userService)
    {
        _adp = adp;
        _db = db;
        _userService = userService;
    }

    public async Task<Unit> Handle(SetCompanyAddressForCurrentUserManualRequest request, CancellationToken cancellationToken)
    {
        var userId = _adp.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot set current user company address because the current user could be determined.");
        }
        
        await _db.ExecuteAsync("UpdateCompanyAddressByUserId", new
        {
            userId,
            request.NameNumber,
            request.AddressLine1,
            request.AddressLine2,
            request.City,
            request.County,
            request.Postcode,
            LocalAuthority = (string)null,
            SubBuildingName = (string)null,
            BuildingName = (string)null,
            BuildingNumber = (string)null,
            Street = (string)null,
            Town = (string)null,
            AdminArea = (string)null,
            UPRN = (string)null,
            AddressLines = (string)null,
            XCoordinate = (string)null,
            YCoordinate = (string)null,
            Toid = (string)null,
            BuildingType = (string)null
        });

        await _userService.SetUserProfileStageCompletionStatus(
            EUserProfileStage.CompanyAddress,
            userId.Value,
            true);

        _adp.SetUserProfileStageCompletionStatus(
            EUserProfileStage.CompanyAddress);
      
        return Unit.Value;
    }
}
