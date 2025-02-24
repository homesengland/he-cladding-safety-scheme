using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetAccountGrantPaidTo;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BankAccount
{
    public class SetAccountGrantPaidToTests
    {
        private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
        private readonly Mock<IDbConnectionWrapper> _connection;

        private readonly SetAccountGrantPaidToHandler _handler;

        public SetAccountGrantPaidToTests()
        {
            _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
            _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
            _handler = new SetAccountGrantPaidToHandler(_connection.Object, _applicationDataProvider.Object);
        }

        [Fact]
        public async Task Handler_Sets_Account_Grant_Paid_To()
        {
            //Arrange
            _applicationDataProvider.Setup(x => x.GetApplicationId())
                                    .Returns(Guid.NewGuid())
                                    .Verifiable();

            _connection.Setup(x => x.ExecuteAsync("UpsertBankAccountGrantPaidTo", It.IsAny<object>()))
                                       .Returns(Task.CompletedTask)
                                       .Verifiable();

            //// Act
            var result = await _handler.Handle(new SetAccountGrantPaidToRequest(), CancellationToken.None);

            _connection.Verify();
            _applicationDataProvider.Verify();
            _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
            _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(),
                                                   It.IsAny<object>()),
                                                   Times.Once);
        }
    }
}
