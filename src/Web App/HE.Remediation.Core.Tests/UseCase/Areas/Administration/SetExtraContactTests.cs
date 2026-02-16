using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using HE.Remediation.Core.UseCase.Areas.Administration.AddExtraContact.SetExtraContact;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Administration;

public class SetExtraContactTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IUserService> _userService;

    private readonly SetExtraContactHandler _handler;
        
    public SetExtraContactTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _userService = new Mock<IUserService>(MockBehavior.Strict);
        _handler = new SetExtraContactHandler(_applicationDataProvider.Object, _connection.Object, _userService.Object);
    }

    [Fact]
    public async Task Handler_Sets_Extra_Contact_Status_And_Updates_DB()
    {
        //Arrange        
        _connection.Setup(x => x.ExecuteAsync("UpdateUserAddSecondaryContactState", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _userService.Setup(x => x.SetUserProfileStageCompletionStatus(It.IsAny<EUserProfileStage>(),
                                                                      It.IsAny<Guid>(),
                                                                      It.IsAny<bool>()))
                                 .Returns(Task.CompletedTask)
                                 .Verifiable();        

        _applicationDataProvider.Setup(x => x.SetUserProfileStageCompletionStatus(It.IsAny<EUserProfileStage>()))                                             
                                             .Verifiable();        

        //// Act
        var result = await _handler.Handle(new SetExtraContactRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetUserId(), Times.Once);
        _userService.Verify(x => x.SetUserProfileStageCompletionStatus(It.IsAny<EUserProfileStage>(),
                                                                      It.IsAny<Guid>(),
                                                                      It.IsAny<bool>()),
                                                                      Times.Once);
    }

    [Fact]
    public async Task Handle_ThrownExceptionForInvalidUserID()
    {
        //Arrange        
        _connection.Setup(x => x.ExecuteAsync("UpdateUserAddSecondaryContactState", It.IsAny<object>()))
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns((Guid?)null)
                                .Verifiable();

        _userService.Setup(x => x.SetUserProfileStageCompletionStatus(It.IsAny<EUserProfileStage>(),
                                                                      It.IsAny<Guid>(),
                                                                      It.IsAny<bool>()))
                                 .Returns(Task.CompletedTask)
                                 .Verifiable();

        _applicationDataProvider.Setup(x => x.SetUserProfileStageCompletionStatus(It.IsAny<EUserProfileStage>()))
                                             .Verifiable();
        
        // Act / Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(new SetExtraContactRequest(), CancellationToken.None).AsTask());
    }
}
