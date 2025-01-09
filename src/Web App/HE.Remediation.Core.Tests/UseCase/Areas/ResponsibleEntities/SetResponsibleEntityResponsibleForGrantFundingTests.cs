using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.EntityResponsibleForGFA.SetResponsibleEntityResponsibleForGrantFunding;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ResponsibleEntities
{
    public class SetResponsibleEntityResponsibleForGrantFundingTests
    {
        private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
        private readonly Mock<IDbConnectionWrapper> _connection;

        private readonly SetResponsibleEntityResponsibleForGrantFundingHandler _handler;

        public SetResponsibleEntityResponsibleForGrantFundingTests()
        {
            _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
            _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

            _handler = new SetResponsibleEntityResponsibleForGrantFundingHandler(_connection.Object, _applicationDataProvider.Object);
        }

        [Fact]
        public async Task Handler_Sets_Responsible_Entity_Responsible_For_Grant_Funding_Signatory_When_User_Is_Responsible()
        {
            //Arrange
            var request = new SetResponsibleEntityResponsibleForGrantFundingRequest
            {
                ResponsibleForGrantFunding = true
            };

            _applicationDataProvider.Setup(x => x.GetApplicationId())
                                    .Returns(Guid.NewGuid())
                                    .Verifiable();

            _connection.Setup(x => x.QuerySingleOrDefaultAsync<bool>("ApplicationHasSignatories", It.IsAny<object>()))
                                       .ReturnsAsync(false)
                                       .Verifiable();

            _connection.Setup(x => x.ExecuteAsync("SetResponsibleEntityResponsibleForGrantFunding", It.IsAny<object>()))
                                       .Returns(Task.CompletedTask)
                                       .Verifiable();
            _connection.Setup(x => x.ExecuteAsync("InsertResponsibleEntitiesGrantFundingDefaultSignatory", It.IsAny<object>()))
                                       .Returns(Task.CompletedTask)
                                       .Verifiable();

            //// Act
            var result = await _handler.Handle(request, CancellationToken.None);

            _connection.Verify();
            _applicationDataProvider.Verify();
            _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
            _connection.Verify(x => x.ExecuteAsync("SetResponsibleEntityResponsibleForGrantFunding",
                                                   It.IsAny<object>()),
                                                   Times.Once);
            _connection.Verify(x => x.ExecuteAsync("InsertResponsibleEntitiesGrantFundingDefaultSignatory",
                                                   It.IsAny<object>()),
                                                   Times.Once);
        }

        [Fact]
        public async Task Handler_Sets_Responsible_Entity_Responsible_For_Grant_Funding_Signatory_When_User_Is_Not_Responsible()
        {
            //Arrange
            var request = new SetResponsibleEntityResponsibleForGrantFundingRequest
            {
                ResponsibleForGrantFunding = false
            };

            _applicationDataProvider.Setup(x => x.GetApplicationId())
                                    .Returns(Guid.NewGuid())
                                    .Verifiable();

            _connection.Setup(x => x.QuerySingleOrDefaultAsync<bool>("ApplicationHasSignatories", It.IsAny<object>()))
                                       .ReturnsAsync(false)
                                       .Verifiable();

            _connection.Setup(x => x.ExecuteAsync("SetResponsibleEntityResponsibleForGrantFunding", It.IsAny<object>()))
                                       .Returns(Task.CompletedTask)
                                       .Verifiable();
            _connection.Setup(x => x.ExecuteAsync("InsertResponsibleEntitiesGrantFundingDefaultSignatory", It.IsAny<object>()))
                                       .Returns(Task.CompletedTask)
                                       .Verifiable();

            //// Act
            var result = await _handler.Handle(request, CancellationToken.None);

            _applicationDataProvider.Verify();
            _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
            _connection.Verify(x => x.ExecuteAsync("SetResponsibleEntityResponsibleForGrantFunding",
                                                   It.IsAny<object>()),
                                                   Times.Once);
            _connection.Verify(x => x.ExecuteAsync("InsertResponsibleEntitiesGrantFundingDefaultSignatory",
                                                   It.IsAny<object>()),
                                                   Times.Never);
        }
    }
}
