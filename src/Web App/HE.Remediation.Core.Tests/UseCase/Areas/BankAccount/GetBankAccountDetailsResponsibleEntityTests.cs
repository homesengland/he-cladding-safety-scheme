using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetBankAccountDetailsResponsibleEntity;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BankAccount
{
    public class GetBankAccountDetailsResponsibleEntityTests
    {
        private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
        private readonly Mock<IDbConnectionWrapper> _connection;

        private readonly GetBankAccountDetailsResponsibleEntityHandler _handler;

        public GetBankAccountDetailsResponsibleEntityTests()
        {
            _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
            _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
            _handler = new GetBankAccountDetailsResponsibleEntityHandler(_connection.Object, _applicationDataProvider.Object);
        }

        [Fact]
        public async Task Handler_Returns_Data_From_DB()
        {
            //Arrange
            var bankDetailsResponse = new GetBankAccountDetailsResponsibleEntityResponse
            {
                NameOnTheAccount = "Test User",
                BankName = "Test Bank",
                BranchName = "Local Branch",
                AccountNumber = "123456",
                SortCode = "102030",
                VatNumber = "GB123456789",
                RepresentationType = Enums.EApplicationRepresentationType.Representative
            };

            _applicationDataProvider.Setup(x => x.GetApplicationId())
                                    .Returns(Guid.NewGuid())
                                    .Verifiable();

            _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetBankAccountDetailsResponsibleEntityResponse>("GetBankAccountDetailsResponsibleEntity", It.IsAny<object>()))
                                    .ReturnsAsync(bankDetailsResponse)
                                    .Verifiable();

            //// Act
            var result = await _handler.Handle(GetBankAccountDetailsResponsibleEntityRequest.Request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);

            var resultValid = ((result != null) &&
                               (result.RepresentationType == Enums.EApplicationRepresentationType.Representative) &&
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
