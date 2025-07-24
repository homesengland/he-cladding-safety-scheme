using Dapper;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService.Enum;
using HE.Remediation.Core.Services.UserService.Model;
using System.Data;

namespace HE.Remediation.Core.Services.UserService;

public class UserService : IUserService
{
    private readonly IDbConnectionWrapper _db;

    public UserService(IDbConnectionWrapper db)
    {
        _db = db;
    }

    public Task RecordSignIn(UserSignInModel signInModel)
        => _db.ExecuteAsync("RecordUserSignIn", signInModel);

    public Task UpdateUserProfileCompletionStages(Guid userId, UserProfileCompletionModel userProfileCompletionModel)
        => _db.ExecuteAsync("SetUserProfileCompletionStatusByUserId", new
        {
            userId,
            userProfileCompletionModel.IsContactInformationComplete,
            userProfileCompletionModel.IsCorrespondenceAddressComplete,
            userProfileCompletionModel.IsResponsibleEntityTypeSelectionComplete,
            userProfileCompletionModel.IsCompanyDetailsComplete,
            userProfileCompletionModel.IsCompanyAddressComplete,
            userProfileCompletionModel.IsSecondaryContactInformationComplete,
            userProfileCompletionModel.IsSecondaryContactSelectionComplete,
            userProfileCompletionModel.IsContactConsentComplete,
            userProfileCompletionModel.IsWantSecondaryContactComplete
        });

    public async Task SetUserProfileStageCompletionStatus(EUserProfileStage profileStage, Guid userId, bool isComplete)
    {
        var profileStageCompletion = await GetUserProfileCompletionData(userId);

        switch (profileStage)
        {
            case EUserProfileStage.ContactInformation:
                profileStageCompletion.IsContactInformationComplete = isComplete;
                break;

            case EUserProfileStage.CorrespondenceAddress:
                profileStageCompletion.IsCorrespondenceAddressComplete = isComplete;
                break;

            case EUserProfileStage.ResponsibleEntityTypeSelection:
                profileStageCompletion.IsResponsibleEntityTypeSelectionComplete = isComplete;
                break;

            case EUserProfileStage.CompanyDetails:
                profileStageCompletion.IsCompanyDetailsComplete = isComplete;
                break;

            case EUserProfileStage.CompanyAddress:
                profileStageCompletion.IsCompanyAddressComplete = isComplete;
                break;

            case EUserProfileStage.SecondaryContactInformation:
                profileStageCompletion.IsSecondaryContactInformationComplete = isComplete;
                break;

            case EUserProfileStage.SecondaryContactSelection:
                profileStageCompletion.IsSecondaryContactSelectionComplete = isComplete;
                break;

            case EUserProfileStage.ContactInfoConsent:
                profileStageCompletion.IsContactConsentComplete = isComplete;
                break;

            case EUserProfileStage.WantSecondaryContact :
                profileStageCompletion.IsWantSecondaryContactComplete = isComplete;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(profileStage));
        }

        await UpdateUserProfileCompletionStages(userId, profileStageCompletion);
    }


    public async Task<bool> IsUserDetailsDataCreated(string auth0UserId)
        => await GetUserDetailsByAuth0UserId(auth0UserId) != null;

    public async Task<bool> IsUserProfileCompletionDataCreated(Guid userId)
        => await GetUserProfileCompletionData(userId) != null;

    public Task<bool> IsUserCompanyDetailsDataCreated(Guid userId)
        => _db.QuerySingleOrDefaultAsync<bool>("UserHasCompanyDetails", new { userId });
    public Task<bool> IsUserCompanyAddressDataCreated(Guid userId)
        => _db.QuerySingleOrDefaultAsync<bool>("UserHasCompanyAddress", new { userId });



    public Task<UserDetailsModel> GetUserDetailsByUserId(Guid userId)
        => _db.QuerySingleOrDefaultAsync<UserDetailsModel>("GetUserByUserId", new { userId });
    public Task<UserDetailsModel> GetUserDetailsByAuth0UserId(string auth0UserId)
        => _db.QuerySingleOrDefaultAsync<UserDetailsModel>("GetUserByAuth0UserId", new { auth0UserId });

    public async Task<UserProfileCompletionModel> GetUserProfileCompletionData(Guid userId)
        => await _db.QuerySingleOrDefaultAsync<UserProfileCompletionModel>("GetUserProfileCompletionByUserId",
            new { userId });



    public async Task<Guid> ScaffoldFirstTimeUserData(string auth0UserId, string emailAddress)
    {
        var p = new DynamicParameters();
        p.Add("Auth0UserId", auth0UserId);
        p.Add("EmailAddress", emailAddress);
        p.Add("NewUserId", dbType: DbType.Guid, direction: ParameterDirection.Output);

        await _db.ExecuteAsync("ScaffoldFirstTimeUserData", p);

        return p.Get<Guid>("NewUserId");
    }

    public Task ScaffoldUserProfileCompletionData(Guid userId)
        => _db.ExecuteAsync("ScaffoldUserProfileCompletionData", new { userId });

    public async Task<UserInvitesPendingModel> IsUserInvitePending(string auth0UserId) 
        => await _db.QuerySingleOrDefaultAsync<UserInvitesPendingModel>("IsUserInvitePending", new { auth0UserId });

    public async Task<UserDetailsModel> GetUserDetailsByCompanyRegistrationNumber(string companyRegistrationNumber)
    {
        return await _db.QuerySingleOrDefaultAsync<UserDetailsModel>("GetUserByCompanyRegistrationNumber", new { companyRegistrationNumber });
    }
}