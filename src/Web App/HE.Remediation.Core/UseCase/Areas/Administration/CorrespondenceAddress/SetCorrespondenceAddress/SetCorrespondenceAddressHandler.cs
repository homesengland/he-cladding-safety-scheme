using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.SetCorrespondenceAddress;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CorrespondanceAddress.SetCorrespondanceAddress;

public class SetCorrespondenceAddressHandler : IRequestHandler<SetCorrespondenceAddressRequest>
{
    private readonly IApplicationDataProvider _adc;
    private readonly IDbConnectionWrapper _db;
    private readonly IUserService _userService;

    public SetCorrespondenceAddressHandler(IApplicationDataProvider adc, IDbConnectionWrapper db, IUserService userService)
    {
        _adc = adc;
        _db = db;
        _userService = userService;
    }

    public async Task<Unit> Handle(SetCorrespondenceAddressRequest request, CancellationToken cancellationToken)
    {
        var userId = _adc.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot set current user company details because the current user could be determined.");
        }

        ParsedAddress parsedAddress = PostCodeUtility.ParseAddressJson(request.SelectedAddressId);
        if (parsedAddress != null)
        {
            await _db.ExecuteAsync("InsertOrUpdateCorrespondanceAddress", new
            {
                userId,
                NameNumber = parsedAddress.NameNumber,
                AddressLine1 = parsedAddress.AddressLine1,
                AddressLine2 = string.Empty,
                City = parsedAddress.City,
                County = string.Empty,
                Postcode = parsedAddress.Postcode
            });

            await _userService.SetUserProfileStageCompletionStatus(
                EUserProfileStage.CorrespondenceAddress,
                userId.Value,
                true);

            _adc.SetUserProfileStageCompletionStatus(EUserProfileStage.CorrespondenceAddress);
        }
        
        return Unit.Value;
    }
}

