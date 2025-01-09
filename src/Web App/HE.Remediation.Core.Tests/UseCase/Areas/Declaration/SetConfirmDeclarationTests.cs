using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResidentialUnits.SetResidentialUnits;
using HE.Remediation.Core.UseCase.Areas.Declaration.SetConfirmDeclaration;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Declaration;

public class SetConfirmDeclarationTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IApplicationRepository> _applicationRepository;

    private readonly SetConfirmDeclarationHandler _handler;

    public SetConfirmDeclarationTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _applicationRepository = new Mock<IApplicationRepository>(MockBehavior.Strict);

        _handler = new SetConfirmDeclarationHandler(_connection.Object,
                                                  _applicationDataProvider.Object, _applicationRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_Confirm_Declaration()
    {
        //Arrange                              
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _applicationRepository.Setup(x => x.GetApplicationStatus(It.IsAny<Guid>()))
            .Returns(Task.FromResult(new GetApplicationStatusResult
            {
                DeclarationConfirmed = false,
                Stage = EApplicationStage.ApplyForGrant,
                Status = EApplicationStatus.ApplicationInProgress,
                Submitted = false
            }))
            .Verifiable();

        _connection.Setup(x => x.ExecuteAsync("UpdateConfirmDeclaration", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationRepository.Setup(x => x.UpdateInternalStatus(It.IsAny<Guid>(), It.IsAny<EApplicationInternalStatus>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        ////// Act
        var result = await _handler.Handle(SetConfirmDeclarationRequest.Request, CancellationToken.None);

        // Assert
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);        
        _connection.Verify();
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Once);
    }
}
