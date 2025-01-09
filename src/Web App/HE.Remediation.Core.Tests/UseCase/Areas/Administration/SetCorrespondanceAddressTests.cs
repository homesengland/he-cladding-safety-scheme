using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondanceAddress.SetCorrespondanceAddress;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.SetCorrespondenceAddress;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Administration;

public class SetCorrespondanceAddressTests
{
    private readonly Mock<IApplicationDataProvider> _adp;
    private readonly Mock<IDbConnectionWrapper> _db;
    private readonly Mock<IUserService> _userService;

    private readonly SetCorrespondenceAddressHandler _handler;

    public SetCorrespondanceAddressTests()
    {
        _db = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _adp = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _userService = new Mock<IUserService>(MockBehavior.Strict);
        _handler = new SetCorrespondenceAddressHandler(_adp.Object, _db.Object, _userService.Object);
    }

    [Fact]
    public async Task Handler_Sets_Correspondance_Address()
    {
        //Arrange        
        _db.Setup(x => x.ExecuteAsync("InsertOrUpdateCorrespondanceAddress",It.IsAny<object>()))                                                          
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
        var result = await _handler.Handle(new SetCorrespondenceAddressRequest
        {
            SelectedAddressId = "{\"Address\":\"MILBURN HOUSE, DEAN STREET, NEWCASTLE UPON TYNE, NE1 1LE\",\"BuildingNumber\":null,\"Locality\":null,\"Street\":\"DEAN STREET\",\"Town\":\"NEWCASTLE UPON TYNE\",\"SubBuildingName\":null,\"BuildingName\":\"MILBURN HOUSE\",\"Postcode\":\"NE1 1LE\",\"Organisation\":null}"
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
