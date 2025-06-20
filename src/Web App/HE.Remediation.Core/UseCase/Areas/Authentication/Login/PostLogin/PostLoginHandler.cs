﻿using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Model;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.Authentication.Login.PostLogin;

public class PostLoginHandler : IRequestHandler<PostLoginRequest, PostLoginResponse>
{
    private readonly IUserService _userService;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public PostLoginHandler(IUserService userService, IApplicationDataProvider applicationDataProvider)
    {
        _userService = userService;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<PostLoginResponse> Handle(PostLoginRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var userId = await _userService.IsUserDetailsDataCreated(request.Auth0UserId)
            ? (await _userService.GetUserDetailsByAuth0UserId(request.Auth0UserId)).UserId
            : await _userService.ScaffoldFirstTimeUserData(request.Auth0UserId, request.EmailAddress);

        if (!await _userService.IsUserProfileCompletionDataCreated(userId))
        {
            await _userService.ScaffoldUserProfileCompletionData(userId);
        }

        var userModel = new UserSignInModel(
            userId,
            request.LoginDateTime,
            request.IpAddress,
            request.UserAgent);

        var userProfileCompletion = await _userService.GetUserProfileCompletionData(userId);
        _applicationDataProvider.SetUserDetails(userId, request.Auth0UserId, userProfileCompletion);

        var userInvitesPending = await _userService.IsUserInvitePending(request.Auth0UserId);

        scope.Complete();

        return new PostLoginResponse
        {
            UserProfileCompletion = userProfileCompletion,
            UserInvitesPending = userInvitesPending
        };
    }

}