
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.SetCorrespondenceAddressManual;

public class SetCorrespondenceAddressManualHandler : IRequestHandler<SetCorrespondenceAddressManualRequest>
{
    private readonly IApplicationDataProvider _adc;
    private readonly IDbConnectionWrapper _db;
    private readonly IUserService _userService;

    public SetCorrespondenceAddressManualHandler(IApplicationDataProvider adc, IDbConnectionWrapper db, IUserService userService)
    {
        _adc = adc;
        _db = db;
        _userService = userService;
    }

    public async Task<Unit> Handle(SetCorrespondenceAddressManualRequest request, CancellationToken cancellationToken)
    {
        var userId = _adc.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot set current user company details because the current user could be determined.");
        }

        await _db.ExecuteAsync("InsertOrUpdateCorrespondanceAddress", new
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
            EUserProfileStage.CorrespondenceAddress,
            userId.Value,
            true);

        _adc.SetUserProfileStageCompletionStatus(EUserProfileStage.CorrespondenceAddress);

        return Unit.Value;
    }
}