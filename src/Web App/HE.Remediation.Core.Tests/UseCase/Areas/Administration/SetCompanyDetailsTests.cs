using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyDetails.SetCompanyDetailsForCurrentUser;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Administration;

public class SetCompanyDetailsTests
{
    private readonly Mock<IApplicationDataProvider> _adp;
    private readonly Mock<IDbConnectionWrapper> _db;
    private readonly Mock<IUserService> _userService;

    private readonly SetCompanyDetailsForCurrentUserHandler _handler;

    public SetCompanyDetailsTests()
    {
        _db = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _adp = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _userService = new Mock<IUserService>(MockBehavior.Strict);
        _handler = new SetCompanyDetailsForCurrentUserHandler(_adp.Object, _db.Object, _userService.Object);
    }

    [Fact]
    public async Task Handler_Sets_Company_Details()
    {
        //Arrange        
        _db.Setup(x => x.ExecuteAsync("UpdateCompanyDetailsByUserId",It.IsAny<object>()))                                                          
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _adp.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _userService.Setup(x => x.SetUserProfileStageCompletionStatus(It.IsAny<EUserProfileStage>(),
                                                                      It.IsAny<Guid>(),
                                                                      It.IsAny<bool>()))
                                 .Returns(Task.CompletedTask)
                                 .Verifiable();

        _adp.Setup(x => x.SetUserProfileStageCompletionStatus(It.IsAny<EUserProfileStage>()))
                                             .Verifiable();

        //// Act
        var result = await _handler.Handle(new SetCompanyDetailsForCurrentUserRequest
        {
            CompanyName = "Test Company",
            CompanyRegistrationNumber = "12345678",
            UserRoleInCompany = "Manager"
        }, CancellationToken.None);

        // Assert
        _db.Verify();
        _adp.Verify(x => x.GetUserId(), Times.Once);
        _adp.Verify(x => x.SetUserProfileStageCompletionStatus(It.IsAny<EUserProfileStage>()),
                                                                      Times.Once);
        _userService.Verify(x => x.SetUserProfileStageCompletionStatus(It.IsAny<EUserProfileStage>(),
                                                                      It.IsAny<Guid>(),
                                                                      It.IsAny<bool>()),
                                                                      Times.Once);
    }
}
