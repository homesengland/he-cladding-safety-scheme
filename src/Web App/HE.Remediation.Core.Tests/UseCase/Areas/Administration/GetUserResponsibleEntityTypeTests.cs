using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Model;
using HE.Remediation.Core.UseCase.Areas.Administration.Profile.GetUserResponsibleEntityType;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Administration;

public class GetUserResponsibleEntityTypeTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IUserService> _userService;

    private readonly GetUserResponsibleEntityTypeHandler _handler;

    public GetUserResponsibleEntityTypeTests()
    {
        _userService = new Mock<IUserService>(MockBehavior.Strict);        
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetUserResponsibleEntityTypeHandler(_userService.Object, _applicationDataProvider.Object);
    }

        [Fact]
    public async Task Handler_Returns_User_Selection_From_DB()
    {
        //Arrange                
        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _userService.Setup(x => x.GetUserDetailsByUserId(It.IsAny<Guid>()))
                                 .ReturnsAsync(() => new UserDetailsModel
                                 {
                                     ResponsibleEntityType = EResponsibleEntityType.Individual

                                 })
                                 .Verifiable();
        
        //// Act
        var result = await _handler.Handle(new GetUserResponsibleEntityTypeRequest(), CancellationToken.None);

        // Assert        
        _applicationDataProvider.Verify(x => x.GetUserId(), Times.Once);
        _userService.Verify(x => x.GetUserDetailsByUserId(It.IsAny<Guid>()), Times.Once);
    }
}
