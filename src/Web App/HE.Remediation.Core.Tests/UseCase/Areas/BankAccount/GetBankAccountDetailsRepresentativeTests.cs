using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetBankAccountDetailsRepresentative;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BankAccount
{
    public class GetBankAccountDetailsRepresentativeTests
    {
        private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
        private readonly Mock<IDbConnectionWrapper> _connection;

        private readonly GetBankAccountDetailsRepresentativeHandler _handler;

        public GetBankAccountDetailsRepresentativeTests()
        {
            _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
            _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
            _handler = new GetBankAccountDetailsRepresentativeHandler(_connection.Object, _applicationDataProvider.Object);
        }

        [Fact]
        public async Task Handler_Returns_Data_From_DB()
        {
            //Arrange
            var bankDetailsResponse = new GetBankAccountDetailsRepresentativeResponse
            {
                NameOnTheAccount = "Test User",
                BankName = "Test Bank",
                BranchName = "Local Branch",
                AccountNumber = "123456",
                SortCode = "102030",
                VatNumber = "GB123456789"
            };

            _applicationDataProvider.Setup(x => x.GetApplicationId())
                                    .Returns(Guid.NewGuid())
                                    .Verifiable();

            _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetBankAccountDetailsRepresentativeResponse>("GetBankAccountDetailsRepresentative", It.IsAny<object>()))
                                    .ReturnsAsync(bankDetailsResponse)
                                    .Verifiable();

            //// Act
            var result = await _handler.Handle(GetBankAccountDetailsRepresentativeRequest.Request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);

            var resultValid = ((result != null) &&
                               (result.NameOnTheAccount == "Test User") &&
                               (result.BankName == "Test Bank") &&
                               (result.BranchName == "Local Branch") &&
                               (result.AccountNumber == "123456") &&
                               (result.SortCode == "102030") &&
                               (result.VatNumber == "GB123456789"));
            Assert.True(resultValid);
            _connection.Verify();
            _applicationDataProvider.Verify();
        }
    }
}
