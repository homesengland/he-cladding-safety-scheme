using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.SetCompanyAddressForCurrentUser;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Administration;

public class SetCompanyAddressForCurrentUserTests
{
    private readonly Mock<IApplicationDataProvider> _adp;
    private readonly Mock<IDbConnectionWrapper> _db;
    private readonly Mock<IUserService> _userService;

    private readonly SetCompanyAddressForCurrentUserHandler _handler;

    public SetCompanyAddressForCurrentUserTests()
    {
        _db = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _adp = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _userService = new Mock<IUserService>(MockBehavior.Strict);
        _handler = new SetCompanyAddressForCurrentUserHandler(_adp.Object, _db.Object, _userService.Object);
    }

    [Fact]
    public async Task Handler_Sets_Company_Address_With_JSON_Content()
    {
        //Arrange        
        _db.Setup(x => x.ExecuteAsync("UpdateCompanyAddressByUserId", It.IsAny<object>()))                  
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
        var addressJson = "{ \"Address\":\"test\", \"BuildingName\": \"123\", \"Street\": \"10 High Road\", " + 
                            "\"AddressLine2\":\"Purley\", \"Town\":\"Croydon\",\"County\":\"Greater London\", " + 
                            "\"Postcode\":\"CR1 3RT\"}";
        var result = await _handler.Handle(new SetCompanyAddressForCurrentUserRequest{ SelectedAddressId = addressJson }, CancellationToken.None);
        
        // Assert
        _db.Verify();
        _adp.Verify(x => x.GetUserId(), Times.Once);
        _userService.Verify(x => x.SetUserProfileStageCompletionStatus(It.IsAny<EUserProfileStage>(),
                                                                      It.IsAny<Guid>(),
                                                                      It.IsAny<bool>()),
                                                                      Times.Once);
    }

    [Fact]
    public async Task Handler_Sets_Company_Address_With_Empty_JSON_Content()
    {
        //Arrange        
        _db.Setup(x => x.ExecuteAsync("UpdateCompanyAddressByUserId", It.IsAny<object>()))                  
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
        var addressJson = "{ }";
        var result = await _handler.Handle(new SetCompanyAddressForCurrentUserRequest{ SelectedAddressId = addressJson }, CancellationToken.None);
        
        // Assert
        _db.Verify();
        _adp.Verify(x => x.GetUserId(), Times.Once);
        _userService.Verify(x => x.SetUserProfileStageCompletionStatus(It.IsAny<EUserProfileStage>(),
                                                                      It.IsAny<Guid>(),
                                                                      It.IsAny<bool>()),
                                                                      Times.Once);
    }
}
