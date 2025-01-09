using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BankAccount.CheckYourAnswers.GetCheckYourAnswers;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BankAccount
{
    public class GetCheckYourAnswersHandlerTests
    {
        private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
        private readonly Mock<IDbConnectionWrapper> _connection;
        private readonly Mock<IApplicationRepository> _applicationRepository;

        private readonly GetCheckYourAnswersHandler _handler;

        public GetCheckYourAnswersHandlerTests()
        {
            _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
            _applicationRepository = new Mock<IApplicationRepository>(MockBehavior.Strict);
            _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
            _handler = new GetCheckYourAnswersHandler(_applicationDataProvider.Object, _connection.Object, _applicationRepository.Object);
        }

        [Fact]
        public async Task Handler_Returns_Data_From_DB()
        {
            //Arrange
            var checkAnswersResponse = new GetCheckYourAnswersResponse
            {
                NameOnTheAccount = "Test User",
                BankName = "Test Bank",
                BranchName = "Local Branch",
                AccountNumber = "123456",
                SortCode = "102030",
                VatNumber = "GB123456789",
                RepresentationType = EResponsibleEntityRepresentationType.ResponsibleEntity,
                BankDetailsRelationship = EBankDetailsRelationship.ResponsibleEntityAccount,
                ReadOnly = false
            };

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

            _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetCheckYourAnswersResponse>("GetBankAccountCheckYourAnswers", It.IsAny<object>()))
                                    .ReturnsAsync(checkAnswersResponse)
                                    .Verifiable();

            //// Act
            var result = await _handler.Handle(GetCheckYourAnswersRequest.Request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);

            var resultValid = ((result != null) &&
                               (result.NameOnTheAccount == "Test User") &&
                               (result.BankName == "Test Bank") &&
                               (result.BranchName == "Local Branch") &&
                               (result.AccountNumber == "123456") &&
                               (result.SortCode == "102030") &&
                               (result.VatNumber == "GB123456789") &&
                               (result.RepresentationType == EResponsibleEntityRepresentationType.ResponsibleEntity) &&
                               (result.BankDetailsRelationship == EBankDetailsRelationship.ResponsibleEntityAccount) &&
                               (!result.ReadOnly));
            Assert.True(resultValid);
            _connection.Verify();
            _applicationDataProvider.Verify();
        }
    }
}
