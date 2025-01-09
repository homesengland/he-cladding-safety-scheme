using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetAccountGrantPaidTo;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BankAccount
{
    public class GetAccountGrantPaidToTests
    {
        private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
        private readonly Mock<IDbConnectionWrapper> _connection;

        private readonly GetAccountGrantPaidToHandler _handler;

        public GetAccountGrantPaidToTests()
        {
            _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
            _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
            _handler = new GetAccountGrantPaidToHandler(_connection.Object, _applicationDataProvider.Object);
        }

        [Fact]
        public async Task Handler_Returns_Data_From_DB()
        {
            //Arrange
            var applicationId = Guid.NewGuid();
            var accountGrantPaidToResponse = new GetAccountGrantPaidToResponse
            {
                ApplicationId = applicationId,
                BankDetailsRelationship = EBankDetailsRelationship.ResponsibleEntityAccount
            };

            _applicationDataProvider.Setup(x => x.GetApplicationId())
                                    .Returns(applicationId)
                                    .Verifiable();

            _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetAccountGrantPaidToResponse>("GetBankAccountGrantPaidTo", It.IsAny<object>()))
                                    .ReturnsAsync(accountGrantPaidToResponse)
                                    .Verifiable();

            //// Act
            var result = await _handler.Handle(GetAccountGrantPaidToRequest.Request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);

            var resultValid = ((result != null) &&
                               (result.ApplicationId == applicationId) &&
                               (result.BankDetailsRelationship == EBankDetailsRelationship.ResponsibleEntityAccount));
            Assert.True(resultValid);
            _connection.Verify();
            _applicationDataProvider.Verify();
        }
    }
}
