using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BankAccount.CheckYourAnswers.SetCheckYourAnswers;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BankAccount
{
    public class SetCheckYourAnswersTests
    {
        private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
        private readonly Mock<IDbConnectionWrapper> _connection;

        private readonly SetCheckYourAnswersHandler _handler;

        public SetCheckYourAnswersTests()
        {
            _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
            _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
            _handler = new SetCheckYourAnswersHandler(_applicationDataProvider.Object, _connection.Object);
        }

        [Fact]
        public async Task Handler_Sets_Bank_Account_Status()
        {
            //Arrange
            _applicationDataProvider.Setup(x => x.GetApplicationId())
                                    .Returns(Guid.NewGuid())
                                    .Verifiable();

            _connection.Setup(x => x.ExecuteAsync("UpdateBankAccountStatus", It.IsAny<object>()))
                                       .Returns(Task.CompletedTask)
                                       .Verifiable();

            //// Act
            var result = await _handler.Handle(SetCheckYourAnswersRequest.Request, CancellationToken.None);

            _connection.Verify();
            _applicationDataProvider.Verify();
            _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
            _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(),
                                                   It.IsAny<object>()),
                                                   Times.Once);
        }
    }
}
