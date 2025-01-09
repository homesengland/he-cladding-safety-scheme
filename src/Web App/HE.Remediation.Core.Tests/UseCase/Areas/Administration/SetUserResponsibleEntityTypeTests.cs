using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using HE.Remediation.Core.Services.UserService.Model;
using HE.Remediation.Core.UseCase.Areas.Administration.Profile.SetUserResponsibleEntityType;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Administration;

public class SetUserResponsibleEntityTypeTests
{    
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IUserService> _userService;

    private readonly SetUserResponsibleEntityTypeHandler _handler;

    public SetUserResponsibleEntityTypeTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _userService = new Mock<IUserService>(MockBehavior.Strict);
        _handler = new SetUserResponsibleEntityTypeHandler(_connection.Object,
                                                           _userService.Object,
                                                           _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Sets_User_Responsible_Entity_Type()
    {
        //Arrange        
        _connection.Setup(x => x.ExecuteAsync("InsertCompanyDetailsForUserId", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();        

        _connection.Setup(x => x.ExecuteAsync("InsertCompanyAddressForUserId", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();        
        
        _connection.Setup(x => x.ExecuteAsync("SetUserResponsibleEntityTypeByUserId", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();        

        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _userService.Setup(x => x.IsUserCompanyDetailsDataCreated(It.IsAny<Guid>()))
                                .Returns(Task.FromResult(false))
                                .Verifiable();        

        _userService.Setup(x => x.IsUserCompanyAddressDataCreated(It.IsAny<Guid>()))
                                .Returns(Task.FromResult(false))
                                .Verifiable();

        _userService.Setup(x => x.GetUserProfileCompletionData(It.IsAny<Guid>()))
                                .Returns(Task.FromResult(new UserProfileCompletionModel
                                {
                                    IsCompanyDetailsComplete = true,
                                    IsCompanyAddressComplete = true,
                                    IsSecondaryContactInformationComplete = true
                                }))
                                .Verifiable();

        _userService.Setup(x => x.UpdateUserProfileCompletionStages(It.IsAny<Guid>(),
                                                                    It.IsAny<UserProfileCompletionModel>()))
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _userService.Setup(x => x.SetUserProfileStageCompletionStatus(It.IsAny<EUserProfileStage>(),
                                                                      It.IsAny<Guid>(),
                                                                      It.IsAny<bool>()))
                                 .Returns(Task.CompletedTask)
                                 .Verifiable();        

        _applicationDataProvider.Setup(x => x.SetUserProfileStageCompletionStatus(It.IsAny<EUserProfileStage>(),
                                                                                  It.IsAny<EResponsibleEntityType>()))                                             
                                             .Verifiable();        

        //// Act
        var result = await _handler.Handle(new SetUserResponsibleEntityTypeRequest
        {
            ResponsibleEntityType = Enums.EResponsibleEntityType.Company
        }   
        , CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetUserId(), Times.AtLeast(2));
        _userService.Verify(x => x.SetUserProfileStageCompletionStatus(It.IsAny<EUserProfileStage>(),
                                                                      It.IsAny<Guid>(),
                                                                      It.IsAny<bool>()),
                                                                      Times.Once);
    }
}
