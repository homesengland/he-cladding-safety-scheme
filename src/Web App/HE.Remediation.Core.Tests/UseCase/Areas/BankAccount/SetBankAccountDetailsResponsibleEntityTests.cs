using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetBankAccountDetailsResponsibleEntity;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BankAccount
{
    public class SetBankAccountDetailsResponsibleEntityTests
    {
        private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
        private readonly Mock<IDbConnectionWrapper> _connection;

        private readonly SetBankAccountDetailsResponsibleEntityHandler _handler;

        public SetBankAccountDetailsResponsibleEntityTests()
        {
            _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
            _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
            _handler = new SetBankAccountDetailsResponsibleEntityHandler(_connection.Object, _applicationDataProvider.Object);
        }

        [Fact]
        public async Task Handler_Sets_Bank_Account_Details()
        {
            //Arrange
            _applicationDataProvider.Setup(x => x.GetApplicationId())
                                    .Returns(Guid.NewGuid())
                                    .Verifiable();

            _connection.Setup(x => x.ExecuteAsync("UpsertBankAccountDetailsResponsibleEntity", It.IsAny<object>()))
                                       .Returns(Task.CompletedTask)
                                       .Verifiable();

            //// Act
            var result = await _handler.Handle(new SetBankAccountDetailsResponsibleEntityRequest(), CancellationToken.None);

            _connection.Verify();
            _applicationDataProvider.Verify();
            _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
            _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(),
                                                   It.IsAny<object>()),
                                                   Times.Once);
        }
    }
}
