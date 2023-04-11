using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CompanyDetails.SetCompanyDetailsForCurrentUser;

public class SetCompanyDetailsForCurrentUserHandler : IRequestHandler<SetCompanyDetailsForCurrentUserRequest>
{
    private readonly IApplicationDataProvider _adc;
    private readonly IDbConnectionWrapper _db;
    private readonly IUserService _userService;

    public SetCompanyDetailsForCurrentUserHandler(IApplicationDataProvider adc, IDbConnectionWrapper db, IUserService userService)
    {
        _adc = adc;
        _db = db;
        _userService = userService;
    }

    public async Task<Unit> Handle(SetCompanyDetailsForCurrentUserRequest request, CancellationToken cancellationToken)
    {
        var userId = _adc.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot set current user company details because the current user could be determined.");
        }

        await _db.ExecuteAsync("UpdateCompanyDetailsByUserId", new
        {
            userId,
            request.CompanyName,
            request.CompanyRegistrationNumber,
            request.UserRoleInCompany
        });

        await _userService.SetUserProfileStageCompletionStatus(
            EUserProfileStage.CompanyDetails,
            userId.Value,
            true);

        _adc.SetUserProfileStageCompletionStatus(
            EUserProfileStage.CompanyDetails);

        return Unit.Value;
    }
}