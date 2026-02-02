using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.Profile.GetUserResponsibleEntityType;

public class GetUserResponsibleEntityTypeHandler : IRequestHandler<GetUserResponsibleEntityTypeRequest, EResponsibleEntityType?>
{
    private readonly IUserService _userService;
    private readonly IApplicationDataProvider _adp;

    public GetUserResponsibleEntityTypeHandler(IUserService userService, IApplicationDataProvider adp)
    {
        _userService = userService;
        _adp = adp;
    }

    public async ValueTask<EResponsibleEntityType?> Handle(GetUserResponsibleEntityTypeRequest request, CancellationToken cancellationToken)
    {
        var userId = _adp.GetUserId();
        
        if (userId is null)
            throw new EntityNotFoundException(
                "Cannot scaffold the current user company details because the current user could be determined.");

        var userDetails = await _userService.GetUserDetailsByUserId(userId.Value);

        return userDetails.ResponsibleEntityType;
    }
}